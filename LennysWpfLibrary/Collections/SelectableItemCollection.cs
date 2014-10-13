using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LennysWpfLibrary.Collections
{
    public class SelectableItemCollection<TItem> : ObservableCollection<TItem>
        where TItem : class, ISelectableCollectionItem
    {
        #region MultiSelect Property Members

        public const string PropertyName_MultiSelect = "MultiSelect";

        private bool _multiSelect = false;

        public bool MultiSelect
        {
            get { return this._multiSelect; }
            set
            {
                this.OnMultiSelectChanged();
                throw new NotImplementedException();
            }
        }

        protected virtual void OnMultiSelectChanged()
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(SelectableItemCollection<TItem>.PropertyName_MultiSelect));
        }

        #endregion

        #region SelectedItems Property Members

        private ObservableCollection<TItem> _innerSelectedItems = new ObservableCollection<TItem>();
        private ReadOnlyObservableCollection<TItem> _selectedItems = null;

        protected ReadOnlyObservableCollection<TItem> SelectedItems
        {
            get
            {
                lock (this._innerSelectedItems)
                {
                    if (this._selectedItems == null)
                        this._selectedItems = new ReadOnlyObservableCollection<TItem>(this._innerSelectedItems);
                }

                return this._selectedItems;
            }
        }

        #endregion

        #region SelectedItem Property Members

        public event CollectionItemEventHandler<TItem> SelectedItemChanged;

        public const string PropertyName_SelectedItem = "SelectedItem";

        private TItem _selectedItem = default(TItem);

        public TItem SelectedItem
        {
            get { return this._selectedItem; }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected void RaiseSelectedItemChanged(TItem value)
        {
            this.OnSelectedItemChanged(new CollectionItemEventArgs<TItem>(value));
        }

        protected virtual void OnSelectedItemChanged(CollectionItemEventArgs<TItem> args)
        {
            if (this.SelectedItemChanged != null)
                this.SelectedItemChanged(this, args);
        }

        #endregion

        #region SelectedIndex Property Members

        public const string PropertyName_SelectedIndex = "SelectedIndex";

        public event EventHandler SelectedIndexChanged;

        private int _selectedIndex = -1;

        public int SelectedIndex
        {
            get { return this._selectedIndex; }
            set
            {
                int previousIndex = this._selectedIndex;
                this._selectedIndex = (value < -1 || value >= this.Count) ? -1 : value;

                if (previousIndex == this._selectedIndex)
                    return;

                TItem previousItem = this._selectedItem;
                this._selectedItem = (value == -1) ? default(TItem) : this[value];

                // TODO: Need to determine best way to update SelectedItems. Thhe Item.IsSelectedChanged event may not be best place because of possibility of a null value at the selected index
                // TODO: If selected index is not -1 and this._selectedItem is not null, then this.SelectedItems needs to contain a null value
                // TODO: With each scenario: MultiSelect is true or false; include whether this.SelectedItems involves containing a null value
                // TODO: Scenario = previousItem == null && this._selectedItem == null
                // TODO: Scenario = previousItem != null && this._selectedItem == null
                // TODO: Scenario = previousItem == null && this._selectedItem != null
                // TODO: Scenario = previousItem != null && this._selectedItem != null && Object.ReferenceEquals(previousItem, this._selectedItem)
                // TODO: Scenario = previousItem != null && this._selectedItem != null && !Object.ReferenceEquals(previousItem, this._selectedItem) && this._selectedItem.IsSelected
                // TODO: Scenario = previousItem != null && this._selectedItem != null && !Object.ReferenceEquals(previousItem, this._selectedItem) && !this._selectedItem.IsSelected

                //if (this.IsUnselectedValue(this._selectedItem))
                //{
                //    foreach (TItem item in this._innerSelectedItems.ToArray())
                //        item.IsSelected = false;
                //}
                //else if (!this.MultiSelect)
                //{
                //    foreach (TItem item in this._innerSelectedItems.Where(i => this.IsSame(i, this._selectedItem)).ToArray())
                //        item.IsSelected = false;
                //}

                this.RaiseSelectedIndexChanged();

                throw new NotImplementedException();
                //if ((this.IsUnselectedValue(previousItem)) ? !this.IsUnselectedValue(this._selectedItem) : (this.IsUnselectedValue(this._selectedItem) || this.IsSame(previousItem, this._selectedItem)))
                //    this.RaiseSelectedItemChanged(this._selectedItem);
            }
        }

        protected void RaiseSelectedIndexChanged()
        {
            this.OnSelectedIndexChanged();
        }

        protected virtual void OnSelectedIndexChanged()
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, EventArgs.Empty);
        }

        #endregion

        #region Constructors

        public SelectableItemCollection()
            : base()
        {
            this.OnInitializeCollection();
        }

        public SelectableItemCollection(IEnumerable<TItem> collection)
            : base(collection)
        {
            this.OnInitializeCollection();
        }

        public SelectableItemCollection(List<TItem> list)
            : base(list)
        {
            this.OnInitializeCollection();
        }

        protected virtual void OnInitializeCollection()
        {
            Type itemType = typeof(TItem);

            //if (itemType.IsByRef)
            //IEnumerable<TItem> fromFirstSelected = this.SkipWhile(i => i == null || !i.IsSelected);

            //this._selectedItem = fromFirstSelected.FirstOrDefault();
            //this._selectedIndex = (this._selectedItem == null) ? -1 : fromFirstSelected.Count();
            //if (!this.MultiSelect)
            //{
            //    foreach (TItem item in fromFirstSelected.Skip(1))
            //        item.IsSelected = false;
            //}

            foreach (TItem item in this.Where(i => i != null))
                this.OnAttachItemEvents(item);
            throw new NotImplementedException();
        }

        #endregion

        #region Item event handlers

        protected virtual void OnAttachItemEvents(TItem item)
        {
            item.IsSelectedChanged += this.Item_IsSelectedChanged;
            item.Selected += this.Item_Selected;
            item.Deselected += this.Item_Deselected;
        }

        protected virtual void OnDetachItemEvents(TItem item)
        {
            item.IsSelectedChanged -= this.Item_IsSelectedChanged;
            item.Selected -= this.Item_Selected;
            item.Deselected -= this.Item_Deselected;
        }

        #region Item.IsSelectedChanged

        private void Item_IsSelectedChanged(object sender, EventArgs e)
        {
            this.RaiseItemIsSelectedChanged(sender as TItem);
        }

        public event CollectionItemEventHandler<TItem> ItemIsSelectedChanged;

        protected void RaiseItemIsSelectedChanged(TItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!this.Contains(item))
                throw new InvalidOperationException("Item is not contained by this collection.");

            if (item.IsSelected)
                this.SelectedItem = item;
            else  if (this.SelectedItem != null && Object.ReferenceEquals(this.SelectedItem, item))
                this.SelectedItem = this._innerSelectedItems.LastOrDefault(i => !Object.ReferenceEquals(i, item));

            this.OnItemIsSelectedChanged(new CollectionItemEventArgs<TItem>(item));
        }

        protected virtual void OnItemIsSelectedChanged(CollectionItemEventArgs<TItem> args)
        {
            if (this.ItemIsSelectedChanged != null)
                this.ItemIsSelectedChanged(this, args);
        }

        #endregion

        #region Item.Selected

        public event CollectionItemEventHandler<TItem> ItemSelected;

        private void Item_Selected(object sender, EventArgs e)
        {
            this.RaiseItemSelected(sender as TItem);
        }

        protected void RaiseItemSelected(TItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!this.Contains(item))
                throw new InvalidOperationException("Item is not contained by this collection.");

            this.OnItemSelected(new CollectionItemEventArgs<TItem>(item));
        }

        protected virtual void OnItemSelected(CollectionItemEventArgs<TItem> args)
        {
            if (this.ItemSelected != null)
                this.ItemSelected(this, args);
        }

        #endregion

        #region Item.Deselected

        public event CollectionItemEventHandler<TItem> ItemDeselected;

        private void Item_Deselected(object sender, EventArgs e)
        {
            this.RaiseItemDeselected(sender as TItem);
        }

        protected void RaiseItemDeselected(TItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!this.Contains(item))
                throw new InvalidOperationException("Item is not contained by this collection.");

            this.OnItemDeselected(new CollectionItemEventArgs<TItem>(item));
        }

        protected virtual void OnItemDeselected(CollectionItemEventArgs<TItem> args)
        {
            if (this.ItemDeselected != null)
                this.ItemDeselected(this, args);
        }

        #endregion

        #endregion

        #region Overrides

        protected override void InsertItem(int index, TItem item)
        {
            throw new NotImplementedException();
            base.InsertItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            throw new NotImplementedException();
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void RemoveItem(int index)
        {
            throw new NotImplementedException();
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, TItem item)
        {
            throw new NotImplementedException();
            base.SetItem(index, item);
        }

        protected override void ClearItems()
        {
            throw new NotImplementedException();
            base.ClearItems();
        }

        #endregion
    }

    public class SelectableItemCollection<TItem, TValue> : SelectableItemCollection<TItem>
        where TItem : class, ISelectableCollectionItem<TValue>
    {
        public SelectableItemCollection() : base() { }
        public SelectableItemCollection(IEnumerable<TItem> collection) : base(collection) { }
        public SelectableItemCollection(List<TItem> list) : base(list) { }
    }
}

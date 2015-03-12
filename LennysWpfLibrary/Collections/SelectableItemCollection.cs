using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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
                if (this._multiSelect == value)
                    return;

                this._multiSelect = value;

                if (!value && this.SelectedItem != null)
                {
                    foreach (TItem item in this.Where(i => i != null && !Object.ReferenceEquals(i, this.SelectedItem)).ToArray())
                        item.IsSelected = false;
                }

                this.OnMultiSelectChanged();
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

        public ReadOnlyObservableCollection<TItem> SelectedItems
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
                this.DetectAndRaiseSelectedEvents(() =>
                {
                    this._selectedItem = value;

                    if (value == null)
                    {
                        this._selectedIndex = this.TakeWhile(i => i != null).Count();
                        if (this._selectedIndex == this.Count)
                            this._selectedIndex = -1;
                    }
                    else
                    {
                        this._selectedIndex = this.TakeWhile(i => i == null || !Object.ReferenceEquals(i, value)).Count();
                        if (this._selectedIndex == this.Count)
                            throw new ArgumentOutOfRangeException();
                    }
                });
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
                this.DetectAndRaiseSelectedEvents(() =>
                {
                    if (value < -1 || value >= this.Count)
                        throw new IndexOutOfRangeException();

                    this._selectedIndex = value;
                    this._selectedItem = (value == -1) ? null : this[value];
                });
            }
        }

        private void DetectAndRaiseSelectedEvents(Action fieldUpdateMethod)
        {
            TItem oldItem, newItem;
            int oldIndex, newIndex;
            Exception error = null;

            lock (this._innerSelectedItems)
            {
                oldIndex = this._selectedIndex;
                oldItem = this._selectedItem;
                newIndex = this._selectedIndex;
                newItem = this._selectedItem;

                try
                {
                    fieldUpdateMethod();
                    newIndex = this._selectedIndex;
                    newItem = this._selectedItem;
                }
                catch (Exception exc)
                {
                    error = exc;
                    this._selectedIndex = oldIndex;
                    this._selectedItem = oldItem;
                }
            }

            if (error != null)
                throw error;

            if ((oldItem == null) ? (newItem != null) : (newItem == null || !Object.ReferenceEquals(oldItem, newItem)))
            {
                if (newItem != null)
                {
                    if (newItem.IsSelected)
                        newItem.IsSelected = true;
                }

                if (!this.MultiSelect && oldItem != null)
                    oldItem.IsSelected = false;

                this.RaiseSelectedItemChanged(newItem);
            }

            if (oldIndex != newIndex)
                this.RaiseSelectedIndexChanged();
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
            this._selectedItem = this.LastOrDefault(i => i.IsSelected);
            this._selectedIndex = (this._selectedItem == null) ? -1 : this.Reverse().SkipWhile(i => i == null || !Object.ReferenceEquals(i, this._selectedItem)).Count() - 1;

            ObjectInstanceComparer<TItem> comparer = new ObjectInstanceComparer<TItem>();
            if (this.MultiSelect)
            {
                foreach (TItem item in this.Where(i => i != null && i.IsSelected).Distinct(comparer))
                    this._innerSelectedItems.Add(item);
            }
            else if (this._selectedItem != null)
                this._innerSelectedItems.Add(this._selectedItem);

            foreach (TItem item in this.Distinct(comparer))
                this.OnAttachItemEvents(item);
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
            {
                if (this.SelectedIndex == -1 || (!this.MultiSelect && !Object.ReferenceEquals(this.SelectedItem, item)))
                    this.SelectedItem = item;

                lock (this._innerSelectedItems)
                {
                    if (item.IsSelected && this.Contains(item) && !this._innerSelectedItems.Contains(item))
                    {
                        if (this._innerSelectedItems.Count == 0 || this.MultiSelect)
                            this._innerSelectedItems.Add(item);
                        else
                        {
                            while (this._innerSelectedItems.Count > 0 && !Object.ReferenceEquals(item, this._innerSelectedItems[0]))
                                this._innerSelectedItems.RemoveAt(0);

                            if (this._innerSelectedItems.Count == 0)
                                this._innerSelectedItems.Add(item);
                            else
                            {
                                while (this._innerSelectedItems.Count > 1)
                                    this._innerSelectedItems.RemoveAt(1);
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.SelectedItem != null && Object.ReferenceEquals(this.SelectedItem, item))
                    this.SelectedItem = this.SelectedItems.LastOrDefault();

                lock (this._innerSelectedItems)
                {
                    if ((!item.IsSelected || !this.Contains(item)) && this._innerSelectedItems.Contains(item))
                        this._innerSelectedItems.Remove(item);
                }
            }

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
            base.InsertItem(index, item);

            if (item.IsSelected && this.SelectedIndex == -1)
                this.SelectedItem = item;

            lock (this._innerSelectedItems)
            {
                if (this.MultiSelect && item != null && item.IsSelected && !this._innerSelectedItems.Any(i => Object.ReferenceEquals(i, item)))
                    this._innerSelectedItems.Add(item);
            }

            if (item != null)
                this.OnAttachItemEvents(item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            TItem replacedItem = (newIndex < 0 || newIndex >= this.Count) ? null : this[newIndex];

            base.MoveItem(oldIndex, newIndex);

            if (replacedItem == null || this.Any(i => i != null && Object.ReferenceEquals(i, replacedItem)))
                return;

            lock (this._innerSelectedItems)
            {
                replacedItem = null;
                int index = this._innerSelectedItems.TakeWhile(i => !Object.ReferenceEquals(i, replacedItem)).Count();
                if (index < this._innerSelectedItems.Count)
                    this._innerSelectedItems.RemoveAt(index);
            }

            if (replacedItem != null)
                this.OnDetachItemEvents(replacedItem);
        }

        protected override void RemoveItem(int index)
        {
            TItem itemToRemove = (index < 0 || index >= this.Count) ? null : this[index];

            base.RemoveItem(index);

            if (itemToRemove == null || this.Any(i => i != null && Object.ReferenceEquals(i, itemToRemove)))
                return;

            lock (this._innerSelectedItems)
            {
                int idx = this._innerSelectedItems.TakeWhile(i => !Object.ReferenceEquals(i, itemToRemove)).Count();
                if (idx < this._innerSelectedItems.Count)
                    this._innerSelectedItems.RemoveAt(index);
            }

            this.OnDetachItemEvents(itemToRemove);

            if (this.SelectedItem != null && Object.ReferenceEquals(itemToRemove, this.SelectedItem))
                this.SelectedItem = this.SelectedItems.LastOrDefault();
        }

        protected override void SetItem(int index, TItem item)
        {
            TItem replacedItem = (index < 0 || index >= this.Count) ? null : this[index];

            bool shouldAttach = item != null && !this.Any(i => i != null && Object.ReferenceEquals(i, item));

            base.SetItem(index, item);

            if ((item == null) ? (replacedItem == null) : (replacedItem != null && Object.ReferenceEquals(item, replacedItem)))
                return;

            if (shouldAttach)
                this.OnAttachItemEvents(item);

            if (replacedItem == null || this.Any(i => i != null && Object.ReferenceEquals(i, replacedItem)))
                return;

            lock (this._innerSelectedItems)
            {
                replacedItem = null;
                int idx = this._innerSelectedItems.TakeWhile(i => !Object.ReferenceEquals(i, replacedItem)).Count();
                if (idx < this._innerSelectedItems.Count)
                    this._innerSelectedItems.RemoveAt(index);
            }

            if (replacedItem != null)
                this.OnDetachItemEvents(replacedItem);
        }

        protected override void ClearItems()
        {
            TItem[] removedItems = this.Where(i => i != null).ToArray();

            base.ClearItems();

            foreach (TItem item in removedItems)
                this.OnDetachItemEvents(item);
        }

        #endregion
    }
}

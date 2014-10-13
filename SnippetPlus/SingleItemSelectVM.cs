using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;

namespace SnippetPlus
{
    public class SingleItemSelectVM<TItem> : DependencyObject
        where TItem : DependencyObject
    {
        #region SelectedIndex Property Members

        public const string PropertyName_SelectedIndex = "SelectedIndex";

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(SingleItemSelectVM<TItem>.PropertyName_SelectedIndex, typeof(int), typeof(SingleItemSelectVM<TItem>),
                new PropertyMetadata(0,
                    (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as SingleItemSelectVM<TItem>).OnSelectedIndexPropertyChanged(e)));

        public int SelectedIndex
        {
            get
            {
                int value = (int)(this.GetValue(SingleItemSelectVM<TItem>.SelectedIndexProperty));
                if (value < 0 && this.Items.Count > 0)
                {
                    value = 1;
                    this.SetValue(SingleItemSelectVM<TItem>.SelectedIndexProperty, value);
                }

                return value;
            }
            set { this.SetValue(SingleItemSelectVM<TItem>.SelectedIndexProperty, value); }
        }

        protected virtual void OnSelectedIndexPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            int value = (int)(args.NewValue);
            if (value >= this.Items.Count)
            {
                this.SelectedIndex = this.Items.Count - 1;
                return;
            }
            else if (value < 0)
            {
                this.SelectedIndex = 0;
                return;
            }

            TItem item = this.Items[value];
            if (!Object.ReferenceEquals(this.SelectedItem, item))
                this.SelectedItem = item;
        }

        #endregion

        #region SelectedItem Property Members

        public const string PropertyName_SelectedItem = "SelectedItem";

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(SingleItemSelectVM<TItem>.PropertyName_SelectedItem, typeof(TItem), typeof(SingleItemSelectVM<TItem>),
                new PropertyMetadata(null,
                    (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as SingleItemSelectVM<TItem>).OnSelectedItemPropertyChanged(e)));

        public TItem SelectedItem
        {
            get
            {
                TItem value = this.GetValue(SingleItemSelectVM<TItem>.SelectedItemProperty) as TItem;
                if (value == null && this.Items.Count > 0)
                {
                    value = this.Items[0];
                    this.SetValue(SingleItemSelectVM<TItem>.SelectedItemProperty, value);
                }

                return value;
            }
            set { this.SetValue(SingleItemSelectVM<TItem>.SelectedItemProperty, value); }
        }

        protected virtual void OnSelectedItemPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            TItem item = args.NewValue as TItem;
            if (item == null)
            {
                if (this.Items.Count > 0)
                    this.SelectedItem = this.Items[0];
                return;
            }

            int index = this.Items.TakeWhile(i => !Object.ReferenceEquals(i, item)).Count();

            if (index == this.Items.Count)
                this.Items.Add(item);

            if (this.SelectedIndex != index)
                this.SelectedIndex = index;
        }

        #endregion

        #region Items Property Members

        public const string PropertyName_Items = "Items";

        public static readonly DependencyPropertyKey ItemsPropertyKey =
            DependencyProperty.RegisterReadOnly(SingleItemSelectVM<TItem>.PropertyName_Items, typeof(ObservableCollection<TItem>), typeof(SingleItemSelectVM<TItem>),
                new PropertyMetadata(null,
                    (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as SingleItemSelectVM<TItem>).OnItemsPropertyChanged(e)));

        public static readonly DependencyProperty ItemsProperty =
            SingleItemSelectVM<TItem>.ItemsPropertyKey.DependencyProperty;

        public ObservableCollection<TItem> Items
        {
            get
            {
                ObservableCollection<TItem> value = this.GetValue(SingleItemSelectVM<TItem>.ItemsProperty) as ObservableCollection<TItem>;

                if (value == null)
                {
                    value = new ObservableCollection<TItem>();
                    this.SetValue(SingleItemSelectVM<TItem>.ItemsPropertyKey, value);
                }

                return value;
            }
            private set { this.SetValue(SingleItemSelectVM<TItem>.ItemsPropertyKey, value); }
        }

        protected virtual void OnItemsPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            ObservableCollection<TItem> value = e.OldValue as ObservableCollection<TItem>;
            if (value != null)
                value.CollectionChanged -= this.Items_CollectionChanged;
            
            value = e.NewValue as ObservableCollection<TItem>;
            if (value == null)
            {
                this.Items = new ObservableCollection<TItem>();
                return;
            }

            value.CollectionChanged += this.Items_CollectionChanged;

            this.Items_CollectionChanged(this.Items, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int index = (this.SelectedItem == null) ? 0 : this.Items.TakeWhile(i => !Object.ReferenceEquals(i, this.SelectedItem)).Count();
            if (index == this.Items.Count)
                index = 0;

            if (index != this.SelectedIndex)
                this.SelectedIndex = index;
        }

        #endregion
    }
}

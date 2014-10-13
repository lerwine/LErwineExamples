using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erwine.Leonard.T.Examples.WpfExamples.ViewModel
{
    // Summary:
    //     Represents a dynamic data collection that provides notifications when items
    //     get added, removed, or when the whole list is refreshed.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the collection.
    [Serializable]
    public class MultiSelectObservableCollection<T> : ObservableCollection<T>
    {
        private ObservableCollection<T> _innerSelectedItems = new ObservableCollection<T>();
        private Dictionary<int, int> _indexTranslations = new Dictionary<int, int>();
        private ReadOnlyObservableCollection<T> _selectedItems = null;

        public ReadOnlyObservableCollection<T> SelectedItens
        {
            get
            {
                if (this._selectedItems == null)
                    this._selectedItems = new ReadOnlyObservableCollection<T>(this._innerSelectedItems);

                return this._selectedItems;
            }
        }

        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class.
        public MultiSelectObservableCollection()
            : base()
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class that contains elements copied from the specified collection.
        //
        // Parameters:
        //   collection:
        //     The collection from which the elements are copied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The collection parameter cannot be null.
        public MultiSelectObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class that contains elements copied from the specified list.
        //
        // Parameters:
        //   list:
        //     The list from which the elements are copied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The list parameter cannot be null.
        public MultiSelectObservableCollection(List<T> list)
            : base()
        {
        }

        //
        // Summary:
        //     Removes all items from the collection.
        protected override void ClearItems()
        {
            this._indexTranslations.Clear();
            this._innerSelectedItems.Clear();
        }

        //
        // Summary:
        //     Inserts an item into the collection at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert.
        protected override void InsertItem(int index, T item) { }

        //
        // Summary:
        //     Moves the item at the specified index to a new location in the collection.
        //
        // Parameters:
        //   oldIndex:
        //     The zero-based index specifying the location of the item to be moved.
        //
        //   newIndex:
        //     The zero-based index specifying the new location of the item.
        protected virtual void MoveItem(int oldIndex, int newIndex) { }

        //
        // Summary:
        //     Removes the item at the specified index of the collection.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        protected override void RemoveItem(int index) { }

        //
        // Summary:
        //     Replaces the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to replace.
        //
        //   item:
        //     The new value for the element at the specified index.
        protected override void SetItem(int index, T item) { }
    }
}

using System;
using System.Windows.Input;

namespace LennysWpfLibrary.Collections
{
    public interface ISelectableCollectionItem
    {
        event EventHandler IsSelectedChanged;
        event EventHandler Selected;
        event EventHandler Deselected;
        bool IsSelected { get; set; }
        ICommand ToggleSelectCommand { get; }
        ICommand SelectCommand { get; }
        ICommand DeselectCommand { get; }
    }

    public interface ISelectableCollectionItem<T> : ISelectableCollectionItem
    {
        T Value { get; set; }
    }
}

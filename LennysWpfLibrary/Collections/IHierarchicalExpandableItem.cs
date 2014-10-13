using System;
using System.Windows.Input;

namespace LennysWpfLibrary.Collections
{
    public interface IHierarchicalExpandableItem
    {
        event EventHandler IsExpandedChanged;
        event EventHandler Expanded;
        event EventHandler Collapsed;
        bool IsExpanded { get; set; }
        ICommand ToggleExpandCommand { get; }
        ICommand ExpandCommand { get; }
        ICommand CollapseCommand { get; }
    }

    public interface IHierarchicalExpandableItem<T> : IHierarchicalExpandableItem, IObservable<T>
    {
        T Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace LennysWpfLibrary.Collections
{
    public abstract class SelectableCollectionItem : DependencyObject, ISelectableCollectionItem
    {
        #region DisplayText Property Members

        public const string PropertyName_DisplayText = "DisplayText";

        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register(SelectableCollectionItem.PropertyName_DisplayText, typeof(string), typeof(SelectableCollectionItem),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as SelectableCollectionItem).OnDisplayTextPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string DisplayText
        {
            get { return this.GetValue(SelectableCollectionItem.DisplayTextProperty) as string; }
            set { this.SetValue(SelectableCollectionItem.DisplayTextProperty, (value == null) ? "" : value); }
        }

        protected virtual void OnDisplayTextPropertyChanged(string oldValue, string newValue)
        {
        }

        #endregion

        #region IsSelected Property Members

        public event EventHandler IsSelectedChanged;

        public static readonly DependencyProperty IsSelectedProperty =
                    DependencyProperty.Register("IsSelected", typeof(bool), typeof(SelectableCollectionItem),
                        new PropertyMetadata(0, SelectableCollectionItem.IsSelected_PropertyChanged));

        public bool IsSelected
        {
            get { return (bool)(this.GetValue(SelectableCollectionItem.IsSelectedProperty)); }
            set { this.SetValue(SelectableCollectionItem.IsSelectedProperty, value); }
        }

        private static void IsSelected_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SelectableCollectionItem).OnIsSelectedChanged((bool)(e.OldValue), (bool)(e.NewValue));
        }

        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            if (this._selectCommand != null)
                this._selectCommand.IsEnabled = !newValue;

            if (this._deselectCommand != null)
                this._deselectCommand.IsEnabled = newValue;

            if (this.IsSelectedChanged != null)
                this.IsSelectedChanged(this, EventArgs.Empty);
        }

        #endregion

        #region ToggleSelect Command Property Members

        public event EventHandler ToggleSelect;

        private Events.RelayCommand _toggleSelectCommand = null;

        public ICommand ToggleSelectCommand
        {
            get
            {
                if (this._toggleSelectCommand == null)
                    this._toggleSelectCommand = new Events.RelayCommand(this.OnToggleSelect);

                return this._toggleSelectCommand;
            }
        }

        protected virtual void OnToggleSelect(object parameter)
        {
            if (this.ToggleSelect != null)
                this.ToggleSelect(this, EventArgs.Empty);

            this.IsSelected = !this.IsSelected;
        }

        #endregion

        #region Select Command Property Members

        public event EventHandler Select;

        public event EventHandler Selected;

        private Events.RelayCommand _selectCommand = null;

        public ICommand SelectCommand
        {
            get
            {
                if (this._selectCommand == null)
                {
                    this._selectCommand = new Events.RelayCommand(this.OnSelect);
                    this._selectCommand.IsEnabled = !this.IsSelected;
                }

                return this._selectCommand;
            }
        }

        protected virtual void OnSelect(object parameter)
        {
            if (this.Select != null)
                this.Select(this, EventArgs.Empty);

            this.IsSelected = true;
        }

        #endregion

        #region Deselect Command Property Members

        public event EventHandler Deselect;

        public event EventHandler Deselected;

        private Events.RelayCommand _deselectCommand = null;

        public ICommand DeselectCommand
        {
            get
            {
                if (this._deselectCommand == null)
                {
                    this._deselectCommand = new Events.RelayCommand(this.OnDeselect);
                    this._deselectCommand.IsEnabled = this.IsSelected;
                }

                return this._deselectCommand;
            }
        }

        protected virtual void OnDeselect(object parameter)
        {
            if (this.Deselect != null)
                this.Deselect(this, EventArgs.Empty);

            this.IsSelected = false;
        }

        #endregion

        public SelectableCollectionItem()
            : base()
        {
            this.OnInitialize(this.DisplayText, this.IsSelected);
        }

        public SelectableCollectionItem(bool isSelected)
            : base()
        {
            this.OnInitialize(this.DisplayText, isSelected);
        }

        public SelectableCollectionItem(string displayText)
            : base()
        {
            this.OnInitialize(displayText, this.IsSelected);
        }

        public SelectableCollectionItem(string displayText, bool isSelected)
            : base()
        {
            this.OnInitialize(displayText, isSelected);
        }

        protected virtual void OnInitialize(string displayText, bool isSelected)
        {
            this.DisplayText = displayText;
            this.IsSelected = isSelected;
        }
    }

    public class SelectableCollectionItem<T> : SelectableCollectionItem, ISelectableCollectionItem<T>
    {
        #region Value Property Members

        public static readonly DependencyProperty ValueProperty =
                    DependencyProperty.Register("Value", typeof(T), typeof(SelectableCollectionItem<T>),
                        new PropertyMetadata(default(T), SelectableCollectionItem<T>.Value_PropertyChanged));

        public T Value
        {
            get { return (T)(this.GetValue(SelectableCollectionItem<T>.ValueProperty)); }
            set { this.SetValue(SelectableCollectionItem<T>.ValueProperty, value); }
        }

        private static void Value_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SelectableCollectionItem<T>).OnValueChanged((T)(e.OldValue), (T)(e.NewValue));
        }

        protected virtual void OnValueChanged(T oldValue, T newValue)
        {
            if (this.AutoUpdateDisplayText)
                this.GetDisplayText(newValue);
        }

        private static Func<T, string> _defaultGetDisplayText = null;

        public static Func<T, string> DefaultGetDisplayText
        {
            get
            {
                if (SelectableCollectionItem<T>._defaultGetDisplayText == null)
                {
                    Type type = typeof(T);

                    if (type.IsEnum)
                        SelectableCollectionItem<T>._defaultGetDisplayText = (T value) => Enum.GetName(value.GetType(), value);
                    else
                    {
                        if (type.IsGenericType)
                        {
                            if (type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                SelectableCollectionItem<T>._defaultGetDisplayText = (T value) => Enum.GetName(value.GetType(), value);
                        }
                    }
                }

                return SelectableCollectionItem<T>._defaultGetDisplayText;
            }
        }

        protected virtual void GetDisplayText(T value)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected bool AutoUpdateDisplayText { get; set; }

        public SelectableCollectionItem(T value)
            : base()
        {
            this.OnInitialize(value, this.DisplayText, this.IsSelected);
        }

        public SelectableCollectionItem(T value, bool isSelected)
            : base(isSelected)
        {
            this.OnInitialize(value, this.DisplayText, isSelected);
        }

        public SelectableCollectionItem(T value, string displayText)
            : base(displayText)
        {
            this.OnInitialize(value, displayText, this.IsSelected);
        }

        public SelectableCollectionItem(T value, string displayText, bool isSelected)
            : base(displayText, isSelected)
        {
            this.OnInitialize(value, displayText, isSelected);
        }

        protected virtual void OnInitialize(T value, string displayText, bool isSelected)
        {
            this.OnInitialize(value, displayText, isSelected, String.IsNullOrWhiteSpace(displayText));
        }

        protected virtual void OnInitialize(T value, string displayText, bool isSelected, bool autoUpdateDisplayText)
        {
            this.AutoUpdateDisplayText = autoUpdateDisplayText;
            this.DisplayText = displayText;
            this.IsSelected = isSelected;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.DisplayText;
        }

        public abstract class ConversionHelper<V> : IEqualityComparer<V>
        {
            public abstract bool Equals(V x, V y);
            public abstract int GetHashCode(V obj);
        }

        private static Func<T, string> _toString = null;

        public static string ToString(T value)
        {
            if (SelectableCollectionItem<T>._toString != null)
                return SelectableCollectionItem<T>._toString(value);

            Type t = typeof(T);

            throw new NotImplementedException();
        }
    }
}

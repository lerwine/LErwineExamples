using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace SnippetPlus
{
    public class DefinePropertyViewModel : DependencyObject
    {
        #region Identifier validation

        private static Regex _validateIdentifierRegex = null;

        public static Regex ValidateIdentifierRegex
        {
            get
            {
                if (DefinePropertyViewModel._validateIdentifierRegex == null)
                    DefinePropertyViewModel._validateIdentifierRegex = new Regex(@"^[a-z_][a-z\d_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                return DefinePropertyViewModel._validateIdentifierRegex;
            }
        }

        private string ValidateIdentifier(string newValue, out string text)
        {
            if (String.IsNullOrWhiteSpace(newValue))
            {
                text = (newValue != null && newValue.Length == 1) ? null : "";
                return DefinePropertyViewModel.IdentifierValidationErrorMessage_NotDefined;
            }

            string s = newValue.Trim();

            text = (s == newValue) ? null : s;
            
            if (DefinePropertyViewModel.ValidateIdentifierRegex.IsMatch(s))
                return "";

            return DefinePropertyViewModel.IdentifierValidationErrorMessage_InvalidName;
        }

        #endregion

        #region PropertyTypeOptions Property Members

        public const string PropertyName_PropertyTypeOptions = "PropertyTypeOptions";

        public static readonly DependencyPropertyKey PropertyTypeOptionsPropertyKey =
            DependencyProperty.RegisterReadOnly(DefinePropertyViewModel.PropertyName_PropertyTypeOptions, typeof(ObservableCollection<PropertyTypeItem>), typeof(DefinePropertyViewModel),
                new PropertyMetadata(new ObservableCollection<PropertyTypeItem>()));

        public static readonly DependencyProperty PropertyTypeOptionsProperty =
            DefinePropertyViewModel.PropertyTypeOptionsPropertyKey.DependencyProperty;

        public ObservableCollection<PropertyTypeItem> PropertyTypeOptions
        {
            get { return (ObservableCollection<PropertyTypeItem>)(this.GetValue(DefinePropertyViewModel.PropertyTypeOptionsProperty)); }
            private set { this.SetValue(DefinePropertyViewModel.PropertyTypeOptionsPropertyKey, value); }
        }

        #endregion

        #region IsNull Property Members

        public const string PropertyName_IsNull = "IsNull";

        public static readonly DependencyProperty IsNullProperty =
            DependencyProperty.Register(DefinePropertyViewModel.PropertyName_IsNull, typeof(bool), typeof(DefinePropertyViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as DefinePropertyViewModel).OnIsNullPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IsNull
        {
            get { return (bool)(this.GetValue(DefinePropertyViewModel.IsNullProperty)); }
            set { this.SetValue(DefinePropertyViewModel.IsNullProperty, value); }
        }

        protected virtual void OnIsNullPropertyChanged(bool oldValue, bool newValue)
        {
        }

        #endregion

        #region RequiresConstantPropertyName Property Members

        public const string PropertyName_RequiresConstantPropertyName = "RequiresConstantPropertyName";

        public static readonly DependencyProperty RequiresConstantPropertyNameProperty =
            DependencyProperty.Register(DefinePropertyViewModel.PropertyName_RequiresConstantPropertyName, typeof(bool), typeof(DefinePropertyViewModel),
                new PropertyMetadata(true));

        public bool RequiresConstantPropertyName
        {
            get { return (bool)(this.GetValue(DefinePropertyViewModel.RequiresConstantPropertyNameProperty)); }
            set { this.SetValue(DefinePropertyViewModel.RequiresConstantPropertyNameProperty, value); }
        }

        #endregion

        #region RequiresBackingField Property Members

        public const string PropertyName_RequiresBackingField = "RequiresBackingField";

        public static readonly DependencyProperty RequiresBackingFieldProperty =
            DependencyProperty.Register(DefinePropertyViewModel.PropertyName_RequiresBackingField, typeof(bool), typeof(DefinePropertyViewModel),
                new PropertyMetadata(true));

        public bool RequiresBackingField
        {
            get { return (bool)(this.GetValue(DefinePropertyViewModel.RequiresBackingFieldProperty)); }
            set { this.SetValue(DefinePropertyViewModel.RequiresBackingFieldProperty, value); }
        }

        #endregion

        #region PropertyNameText Property Members

        public const string PropertyName_PropertyNameText = "PropertyNameText";

        public static readonly DependencyProperty PropertyNameTextProperty =
            DependencyProperty.Register(DefinePropertyViewModel.PropertyName_PropertyNameText, typeof(string), typeof(DefinePropertyViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as DefinePropertyViewModel).OnPropertyNameTextPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string PropertyNameText
        {
            get { return this.GetValue(DefinePropertyViewModel.PropertyNameTextProperty) as string; }
            set { this.SetValue(DefinePropertyViewModel.PropertyNameTextProperty, value); }
        }

        protected virtual void OnPropertyNameTextPropertyChanged(string oldValue, string newValue)
        {
            string text;
            this.PropertyNameErrorMessage = this.ValidateIdentifier(newValue, out text);
            if (text != null)
            {
                this.PropertyNameText = text;
                return;
            }

            if (this.PropertyNameErrorMessage.Length > 0)
                return;

            if (Char.IsLower(newValue[0]))
            {
                this.PropertyNameText = new String(newValue.Take(1).Select(c => Char.ToUpper(c)).Concat(newValue.Skip(1)).ToArray());
                return;
            }

            string previousLc = (oldValue == null) ? "" : oldValue.Trim();
            if (previousLc.Length > 0)
                previousLc = new String(previousLc.Take(1).Select(c => Char.ToLower(c)).Concat(previousLc.Skip(1)).ToArray());

            if (this.LCFirstPropertyNameText == previousLc)
                this.LCFirstPropertyNameText = new String(newValue.Take(1).Select(c => Char.ToLower(c)).Concat(newValue.Skip(1)).ToArray());
        }

        #endregion

        #region PropertyNameErrorMessage Property Members

        public const string PropertyName_PropertyNameErrorMessage = "PropertyNameErrorMessage";
        public const string IdentifierValidationErrorMessage_NotDefined = "Property name not provided.";
        public const string IdentifierValidationErrorMessage_InvalidName = "Invalid identifier name.";

        public static readonly DependencyPropertyKey PropertyNameErrorMessagePropertyKey =
            DependencyProperty.RegisterReadOnly(DefinePropertyViewModel.PropertyName_PropertyNameErrorMessage, typeof(string), typeof(DefinePropertyViewModel),
                new PropertyMetadata(DefinePropertyViewModel.IdentifierValidationErrorMessage_NotDefined));

        public static readonly DependencyProperty PropertyNameErrorMessageProperty =
          DefinePropertyViewModel.PropertyNameErrorMessagePropertyKey.DependencyProperty;

        public string PropertyNameErrorMessage
        {
            get { return this.GetValue(DefinePropertyViewModel.PropertyNameErrorMessageProperty) as string; }
            private set { this.SetValue(DefinePropertyViewModel.PropertyNameErrorMessagePropertyKey, value); }
        }

        #endregion

        #region LCFirstPropertyNameText Property Members

        public const string PropertyName_LCFirstPropertyNameText = "LCFirstPropertyNameText";

        public static readonly DependencyProperty LCFirstPropertyNameTextProperty =
            DependencyProperty.Register(DefinePropertyViewModel.PropertyName_LCFirstPropertyNameText, typeof(string), typeof(DefinePropertyViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as DefinePropertyViewModel).OnLCFirstPropertyNameTextPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string LCFirstPropertyNameText
        {
            get { return this.GetValue(DefinePropertyViewModel.LCFirstPropertyNameTextProperty) as string; }
            set { this.SetValue(DefinePropertyViewModel.LCFirstPropertyNameTextProperty, value); }
        }

        protected virtual void OnLCFirstPropertyNameTextPropertyChanged(string oldValue, string newValue)
        {
            string text;
            this.LCFirstPropertyNameErrorMessage = this.ValidateIdentifier(newValue, out text);
            if (text != null)
            {
                this.LCFirstPropertyNameText = text;
                return;
            }

            if (this.LCFirstPropertyNameErrorMessage.Length > 0)
                return;

            if (Char.IsLower(newValue[0]))
            {
                this.LCFirstPropertyNameText = new String(newValue.Take(1).Select(c => Char.ToLower(c)).Concat(newValue.Skip(1)).ToArray());
                return;
            }

            string previousLc = (oldValue == null) ? "" : oldValue.Trim();
            if (previousLc.Length > 0)
                previousLc = new String(previousLc.Take(1).Select(c => Char.ToUpper(c)).Concat(previousLc.Skip(1)).ToArray());

            if (this.PropertyNameText == previousLc)
                this.PropertyNameText = new String(newValue.Take(1).Select(c => Char.ToUpper(c)).Concat(newValue.Skip(1)).ToArray());
        }

        #endregion

        #region LCFirstPropertyNameErrorMessage Property Members

        public const string PropertyName_LCFirstPropertyNameErrorMessage = "LCFirstPropertyNameErrorMessage";

        public static readonly DependencyPropertyKey LCFirstPropertyNameErrorMessagePropertyKey =
            DependencyProperty.RegisterReadOnly(DefinePropertyViewModel.PropertyName_LCFirstPropertyNameErrorMessage, typeof(string), typeof(DefinePropertyViewModel),
                new PropertyMetadata(""));

        public static readonly DependencyProperty LCFirstPropertyNameErrorMessageProperty =
          DefinePropertyViewModel.LCFirstPropertyNameErrorMessagePropertyKey.DependencyProperty;

        public string LCFirstPropertyNameErrorMessage
        {
            get { return this.GetValue(DefinePropertyViewModel.LCFirstPropertyNameErrorMessageProperty) as string; }
            private set { this.SetValue(DefinePropertyViewModel.LCFirstPropertyNameErrorMessagePropertyKey, value); }
        }

        #endregion

    }
}

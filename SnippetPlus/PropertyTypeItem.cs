using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace SnippetPlus
{
    public class PropertyTypeItem : DependencyObject
    {
        #region DisplayText Property Members

        public const string PropertyName_DisplayText = "DisplayText";

        public static readonly DependencyPropertyKey DisplayTextPropertyKey =
            DependencyProperty.RegisterReadOnly(PropertyTypeItem.PropertyName_DisplayText, typeof(string), typeof(PropertyTypeItem),
                new PropertyMetadata(""));

        public static readonly DependencyProperty DisplayTextProperty =
          PropertyTypeItem.DisplayTextPropertyKey.DependencyProperty;

        public string DisplayText
        {
            get { return this.GetValue(PropertyTypeItem.DisplayTextProperty) as string; }
            private set { this.SetValue(PropertyTypeItem.DisplayTextPropertyKey, value); }
        }

        #endregion

        #region DefaultValue Property Members

        public const string PropertyName_DefaultValue = "DefaultValue";

        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.Register(PropertyTypeItem.PropertyName_DefaultValue, typeof(string), typeof(PropertyTypeItem),
                new PropertyMetadata(""));

        public string DefaultValue
        {
            get { return this.GetValue(PropertyTypeItem.DefaultValueProperty) as string; }
            set { this.SetValue(PropertyTypeItem.DefaultValueProperty, value); }
        }

        #endregion

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(PropertyTypeItem.PropertyName_Value, typeof(string), typeof(PropertyTypeItem),
                new PropertyMetadata(""));

        public string Value
        {
            get { return this.GetValue(PropertyTypeItem.ValueProperty) as string; }
            set { this.SetValue(PropertyTypeItem.ValueProperty, value); }
        }

        #endregion

        #region TypeName Property Members

        public const string PropertyName_TypeName = "TypeName";

        public static readonly DependencyProperty TypeNameProperty =
            DependencyProperty.Register(PropertyTypeItem.PropertyName_TypeName, typeof(string), typeof(PropertyTypeItem),
                new PropertyMetadata("string"));

        public string TypeName
        {
            get { return this.GetValue(PropertyTypeItem.TypeNameProperty) as string; }
            set { this.SetValue(PropertyTypeItem.TypeNameProperty, value); }
        }

        #endregion

    }
}

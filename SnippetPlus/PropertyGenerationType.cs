using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SnippetPlus
{
    public class PropertyGenerationType : DependencyObject
    {
        #region DisplayText Property Members

        public const string PropertyName_DisplayText = "DisplayText";

        public static readonly DependencyPropertyKey DisplayTextPropertyKey =
            DependencyProperty.RegisterReadOnly(PropertyGenerationType.PropertyName_DisplayText, typeof(string), typeof(PropertyGenerationType),
                new PropertyMetadata(""));

        public static readonly DependencyProperty DisplayTextProperty =
          PropertyGenerationType.DisplayTextPropertyKey.DependencyProperty;

        public string DisplayText
        {
            get { return this.GetValue(PropertyGenerationType.DisplayTextProperty) as string; }
            private set { this.SetValue(PropertyGenerationType.DisplayTextPropertyKey, value); }
        }

        #endregion

        #region Name Property Members

        public const string PropertyName_Name = "Name";

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(PropertyGenerationType.PropertyName_Name, typeof(string), typeof(PropertyGenerationType),
                new PropertyMetadata("string"));

        public string Name
        {
            get { return this.GetValue(PropertyGenerationType.NameProperty) as string; }
            set { this.SetValue(PropertyGenerationType.NameProperty, value); }
        }

        #endregion

        #region CanBeNull Property Members

        public const string PropertyName_CanBeNull = "CanBeNull";

        public static readonly DependencyProperty CanBeNullProperty =
            DependencyProperty.Register(PropertyGenerationType.PropertyName_CanBeNull, typeof(bool), typeof(PropertyGenerationType),
                new PropertyMetadata(true));

        public bool CanBeNull
        {
            get { return (bool)(this.GetValue(PropertyGenerationType.CanBeNullProperty)); }
            set { this.SetValue(PropertyGenerationType.CanBeNullProperty, value); }
        }

        #endregion

        #region ConstructorLeadText Property Members

        public const string PropertyName_ConstructorLeadText = "ConstructorLeadText";

        public static readonly DependencyProperty ConstructorLeadTextProperty =
            DependencyProperty.Register(PropertyGenerationType.PropertyName_ConstructorLeadText, typeof(string), typeof(PropertyGenerationType),
                new PropertyMetadata("\""));

        public string ConstructorLeadText
        {
            get { return this.GetValue(PropertyGenerationType.ConstructorLeadTextProperty) as string; }
            set { this.SetValue(PropertyGenerationType.ConstructorLeadTextProperty, value); }
        }

        #endregion

        #region ConstructorTrailText Property Members

        public const string PropertyName_ConstructorTrailText = "ConstructorTrailText";

        public static readonly DependencyProperty ConstructorTrailTextProperty =
            DependencyProperty.Register(PropertyGenerationType.PropertyName_ConstructorTrailText, typeof(string), typeof(PropertyGenerationType),
                new PropertyMetadata("\""));

        public string ConstructorTrailText
        {
            get { return this.GetValue(PropertyGenerationType.ConstructorTrailTextProperty) as string; }
            set { this.SetValue(PropertyGenerationType.ConstructorTrailTextProperty, value); }
        }

        #endregion

    }
}

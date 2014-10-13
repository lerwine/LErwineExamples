using System.Windows;

namespace SnippetPlus.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.String"/> source values to <see cref="System.Boolean"/> target values.
    /// </summary>
    public class StringToBoolValueConverter : StringToStructValueConverter<bool>
    {
        /// <summary>
        /// Identifies the <see cref="SnippetPlus.Converters.StringToBoolValueConverter.NonEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(bool?), typeof(StringToBoolValueConverter), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="SnippetPlus.Converters.StringToBoolValueConverter.EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(bool?), typeof(StringToBoolValueConverter), new PropertyMetadata(false));

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for non-empty strings
        /// </summary>
        public override bool? NonEmptyValue
        {
            get { return (bool?)(this.GetValue(StringToBoolValueConverter.NonEmptyValueProperty)); }
            set { this.SetValue(StringToBoolValueConverter.NonEmptyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for empty strings
        /// </summary>
        public override bool? EmptyValue
        {
            get { return (bool?)(this.GetValue(StringToBoolValueConverter.EmptyValueProperty)); }
            set { this.SetValue(StringToBoolValueConverter.EmptyValueProperty, value); }
        }
    }
}

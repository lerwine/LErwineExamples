using System.Windows;

namespace SnippetPlus.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Boolean"/> source values to <see cref="System.Windows.Visibility"/> target values.
    /// </summary>
    public class BoolToVisibilityValueConverter : StructToStructValueConverter<bool, Visibility>
    {
        /// <summary>
        /// Identifies the <see cref="SnippetPlus.Converters.BoolToVisibilityValueConverter.TrueValue"/> property.
        /// </summary>
        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register("TrueValue", typeof(Visibility?), typeof(BoolToVisibilityValueConverter), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="SnippetPlus.Converters.BoolToVisibilityValueConverter.FalseValue"/> property.
        /// </summary>
        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register("FalseValue", typeof(Visibility?), typeof(BoolToVisibilityValueConverter), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Windows.Visibility&gt;"/> value to use for true <see cref="System.Boolean"/> source values.
        /// </summary>
        public Visibility? TrueValue
        {
            get { return (Visibility?)(this.GetValue(BoolToVisibilityValueConverter.TrueValueProperty)); }
            set { this.SetValue(BoolToVisibilityValueConverter.TrueValueProperty, value); }
        }

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Windows.Visibility&gt;"/> value to use for false <see cref="System.Boolean"/> source values.
        /// </summary>
        public Visibility? FalseValue
        {
            get { return (Visibility?)(this.GetValue(BoolToVisibilityValueConverter.FalseValueProperty)); }
            set { this.SetValue(BoolToVisibilityValueConverter.FalseValueProperty, value); }
        }

        protected override Visibility? OnConvertToTarget(bool value)
        {
            return (value) ? this.TrueValue : this.FalseValue;
        }
    }
}

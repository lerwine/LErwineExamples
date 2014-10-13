using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Windows.Visibility"/> source values to <see cref="System.Boolean"/> target values.
    /// </summary>
    public class VisibilityToBoolValueConverter : StructToStructValueConverter<Visibility, bool>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.VisibilityToBoolValueConverter.VisibleValue"/> property.
        /// </summary>
        public static readonly DependencyProperty VisibleValueProperty =
            DependencyProperty.Register("VisibleValue", typeof(bool?), typeof(VisibilityToBoolValueConverter), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.VisibilityToBoolValueConverter.CollapsedValue"/> property.
        /// </summary>
        public static readonly DependencyProperty CollapsedValueProperty =
            DependencyProperty.Register("CollapsedValue", typeof(bool?), typeof(VisibilityToBoolValueConverter), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.VisibilityToBoolValueConverter.HiddenValue"/> property.
        /// </summary>
        public static readonly DependencyProperty HiddenValueProperty =
            DependencyProperty.Register("HiddenValue", typeof(bool?), typeof(VisibilityToBoolValueConverter), new PropertyMetadata(false));

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for <see cref="System.Windows.Visibility.Visible"/> source values.
        /// </summary>
        public bool? VisibleValue
        {
            get { return (bool?)(this.GetValue(VisibilityToBoolValueConverter.VisibleValueProperty)); }
            set { this.SetValue(VisibilityToBoolValueConverter.VisibleValueProperty, value); }
        }

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for <see cref="System.Windows.Visibility.Collapsed"/> source values.
        /// </summary>
        public bool? CollapsedValue
        {
            get { return (bool?)(this.GetValue(VisibilityToBoolValueConverter.CollapsedValueProperty)); }
            set { this.SetValue(VisibilityToBoolValueConverter.CollapsedValueProperty, value); }
        }

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for <see cref="System.Windows.Visibility.Hidden"/> source values.
        /// </summary>
        public bool? HiddenValue
        {
            get { return (bool?)(this.GetValue(VisibilityToBoolValueConverter.HiddenValueProperty)); }
            set { this.SetValue(VisibilityToBoolValueConverter.HiddenValueProperty, value); }
        }

        protected override bool? OnConvertToTarget(Visibility value)
        {
            switch (value)
            {
                case Visibility.Visible:
                    return this.VisibleValue;
                case Visibility.Hidden:
                    return this.HiddenValue;
            }

            return this.CollapsedValue;
        }
    }
}

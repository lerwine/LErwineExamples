using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Array"/> and <see cref="System.Collections.ICollection"/> source values to <see cref="System.Windows.Visibility"/> target values.
    /// </summary>
    public class CollectionToVisibilityValueConverter : CollectionToStructValueConverter<Visibility>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.CollectionToVisibilityValueConverter.NonEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(Visibility?), typeof(CollectionToVisibilityValueConverter), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.CollectionToVisibilityValueConverter.EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(Visibility?), typeof(CollectionToVisibilityValueConverter), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// <see cref="Nullable&lt;System.Windows.Visibility&gt;"/> value to use for non-empty, non-null collections
        /// </summary>
        public override Visibility? NonEmptyValue
        {
            get { return (Visibility?)(this.GetValue(CollectionToVisibilityValueConverter.NonEmptyValueProperty)); }
            set { this.SetValue(CollectionToVisibilityValueConverter.NonEmptyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="Nullable&lt;System.Windows.Visibility&gt;"/> value to use for empty, non-null collections
        /// </summary>
        public override Visibility? EmptyValue
        {
            get { return (Visibility?)(this.GetValue(CollectionToVisibilityValueConverter.EmptyValueProperty)); }
            set { this.SetValue(CollectionToVisibilityValueConverter.EmptyValueProperty, value); }
        }
    }
}

using System.Windows;

namespace SnippetPlus.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Array"/> and <see cref="System.Collections.ICollection"/> source values to <see cref="System.Boolean"/> target values.
    /// </summary>
    public class CollectionToBoolValueConverter : CollectionToStructValueConverter<bool>
    {
        /// <summary>
        /// Identifies the <see cref="SnippetPlus.Converters.CollectionToBoolValueConverter.NonEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(bool?), typeof(CollectionToBoolValueConverter), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="SnippetPlus.Converters.CollectionToBoolValueConverter.EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(bool?), typeof(CollectionToBoolValueConverter), new PropertyMetadata(false));

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for non-empty, non-null collections
        /// </summary>
        public override bool? NonEmptyValue
        {
            get { return (bool?)(this.GetValue(CollectionToBoolValueConverter.NonEmptyValueProperty)); }
            set { this.SetValue(CollectionToBoolValueConverter.NonEmptyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for empty, non-null collections
        /// </summary>
        public override bool? EmptyValue
        {
            get { return (bool?)(this.GetValue(CollectionToBoolValueConverter.EmptyValueProperty)); }
            set { this.SetValue(CollectionToBoolValueConverter.EmptyValueProperty, value); }
        }
    }
}

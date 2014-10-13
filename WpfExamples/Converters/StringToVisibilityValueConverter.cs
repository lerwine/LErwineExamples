using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.String"/> source values to <see cref="System.Windows.Visibility"/> target values.
    /// </summary>
    public class StringToVisibilityValueConverter : StringToStructValueConverter<Visibility>
    {
        public static readonly DependencyProperty EmptyValueProperty =
               DependencyProperty.Register("EmptyValue", typeof(Visibility?), typeof(StringToVisibilityValueConverter), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(Visibility?), typeof(StringToVisibilityValueConverter), new PropertyMetadata(Visibility.Visible));

        public override Visibility? EmptyValue
        {
            get { return (Visibility?)(this.GetValue(StringToVisibilityValueConverter.EmptyValueProperty)); }
            set { this.SetValue(StringToVisibilityValueConverter.EmptyValueProperty, value); }
        }

        public override Visibility? NonEmptyValue
        {
            get { return (Visibility?)(this.GetValue(StringToVisibilityValueConverter.NonEmptyValueProperty)); }
            set { this.SetValue(StringToVisibilityValueConverter.NonEmptyValueProperty, value); }
        }
    }
}

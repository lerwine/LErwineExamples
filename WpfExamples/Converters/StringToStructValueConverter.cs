using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    public abstract class StringToStructValueConverter<TTarget> : ValueConverterBase<string, TTarget?>
        where TTarget : struct
    {
        public static readonly DependencyProperty NullValueProperty =
               DependencyProperty.Register("NullValue", typeof(TTarget?), typeof(StringToStructValueConverter<TTarget>), new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyStringOptionProperty =
            DependencyProperty.Register("EmptyStringOption", typeof(StringEmptyOption), typeof(StringToStructValueConverter<TTarget>), new PropertyMetadata(StringEmptyOption.NullOrWhiteSpace));

        public override TTarget? NullValue
        {
            get { return (TTarget?)(this.GetValue(StringToStructValueConverter<TTarget>.NullValueProperty)); }
            set { this.SetValue(StringToStructValueConverter<TTarget>.NullValueProperty, value); }
        }

        public StringEmptyOption EmptyStringOption
        {
            get { return (StringEmptyOption)(this.GetValue(StringToStructValueConverter<TTarget>.EmptyStringOptionProperty)); }
            set { this.SetValue(StringToStructValueConverter<TTarget>.EmptyStringOptionProperty, value); }
        }

        public abstract TTarget? EmptyValue { get; set; }

        public abstract TTarget? NonEmptyValue { get; set; }

        protected override TTarget? OnConvertToTarget(string value)
        {
            switch (this.EmptyStringOption)
            {
                case StringEmptyOption.Null:
                    return this.NonEmptyValue;
                case StringEmptyOption.NullOrEmpty:
                    return (value.Length == 0) ? this.EmptyValue : this.NonEmptyValue;
            }

            return (value.Length == 0 || value.Trim().Length == 0) ? this.EmptyValue : this.NonEmptyValue;
        }
    }
}

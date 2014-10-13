using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    public abstract class StructToStructValueConverter<TSource, TTarget> : ValueConverterBase<TSource?, TTarget?>
        where TSource : struct
        where TTarget : struct
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.StructToStructValueConverter&lt;TSource, TTarget&gt;.NullValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NullValueProperty =
               DependencyProperty.Register("NullValue", typeof(TTarget?), typeof(StructToStructValueConverter<TSource, TTarget>), new PropertyMetadata(null));

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for null source values.
        /// </summary>
        public override TTarget? NullValue
        {
            get { return (TTarget?)(this.GetValue(StructToStructValueConverter<TSource, TTarget>.NullValueProperty)); }
            set { this.SetValue(StructToStructValueConverter<TSource, TTarget>.NullValueProperty, value); }
        }

        protected abstract TTarget? OnConvertToTarget(TSource value);

        protected override TTarget? OnConvertToTarget(TSource? value)
        {
            if (value.HasValue)
                return this.NullValue;

            return this.OnConvertToTarget(value.Value);
        }
    }
}

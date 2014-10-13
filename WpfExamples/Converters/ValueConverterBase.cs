using System;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    public abstract class ValueConverterBase<TSource, TTarget> : DependencyObject, System.Windows.Data.IValueConverter
    {
        public abstract TTarget NullValue { get; set; }

        /// <summary>
        /// Converts <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TSource"/> value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A <typeparamref name="TTarget"/> value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (targetType != null && targetType.IsInstanceOfType(value))
                return value;

            object source = (value is TSource) ? value : this.OnOnConvertToSource(value);

            TTarget result = (source == null) ? this.NullValue : this.OnConvertToTarget((TSource)source);

            if (result != null && targetType != null && !targetType.IsInstanceOfType(result))
                return System.Convert.ChangeType(result, targetType);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        protected abstract TTarget OnConvertToTarget(TSource value);

        protected virtual TSource OnOnConvertToSource(object value)
        {
            return (TSource)(System.Convert.ChangeType(value, typeof(TSource)));
        }
    }
}

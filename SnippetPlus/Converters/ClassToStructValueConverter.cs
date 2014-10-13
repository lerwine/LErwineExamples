using System.Windows;

namespace SnippetPlus.Converters
{
    public abstract class ClassToStructValueConverter<TSource, TTarget> : ValueConverterBase<TSource, TTarget?>
        where TTarget : struct
        where TSource : class
    {
        public static readonly DependencyProperty NullValueProperty =
               DependencyProperty.Register("NullValue", typeof(TTarget?), typeof(ClassToStructValueConverter<TSource, TTarget>), new PropertyMetadata(null));

        public override TTarget? NullValue
        {
            get { return (TTarget?)(this.GetValue(ClassToStructValueConverter<TSource, TTarget>.NullValueProperty)); }
            set { this.SetValue(ClassToStructValueConverter<TSource, TTarget>.NullValueProperty, value); }
        }

        protected abstract override TTarget? OnConvertToTarget(TSource value);
    }
}

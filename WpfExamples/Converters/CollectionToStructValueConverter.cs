using System;
using System.Collections;

namespace Erwine.Leonard.T.Examples.WpfExamples.Converters
{
    public abstract class CollectionToStructValueConverter<TTarget> : ClassToStructValueConverter<ICollection, TTarget>
        where TTarget : struct
    {
        public abstract TTarget? EmptyValue { get; set; }
        public abstract TTarget? NonEmptyValue { get; set; }

        protected override TTarget? OnConvertToTarget(ICollection value)
        {
            return (((value is Array) ? (value as Array).Length : value.Count) == 0) ? this.NonEmptyValue : this.EmptyValue;
        }
    }
}

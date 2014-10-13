using System;

namespace LennysWpfLibrary.Generics
{
    public class ComparableGenericEqualityHelper<T> : BaseGenericEqualityHelper<T>
        where T : IComparable<T>
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return x.CompareTo(y) == 0;
        }
    }
}

using System;

namespace LennysWpfLibrary.Generics
{
    public class GComparableGenericEqualityHelper<T> : BaseGenericEqualityHelper<T>
        where T : IComparable
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return x.CompareTo(y) == 0;
        }
    }
}

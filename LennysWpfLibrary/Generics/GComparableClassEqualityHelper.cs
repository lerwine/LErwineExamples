using System;

namespace LennysWpfLibrary.Generics
{
    public class GComparableClassEqualityHelper<T> : BaseClassEqualityHelper<T>
        where T : class, IComparable
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return x.CompareTo(y) == 0;
        }
    }
}

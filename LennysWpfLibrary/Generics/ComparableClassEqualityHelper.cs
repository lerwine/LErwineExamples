using System;

namespace LennysWpfLibrary.Generics
{
    public class ComparableClassEqualityHelper<T> : BaseClassEqualityHelper<T>
        where T : class, IComparable<T>
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return x.CompareTo(y) == 0;
        }
    }
}

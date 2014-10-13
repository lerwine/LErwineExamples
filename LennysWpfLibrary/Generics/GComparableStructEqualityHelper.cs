using System;

namespace LennysWpfLibrary.Generics
{
    public class GComparableStructEqualityHelper<T> : BaseStructEqualityHelper<T>
        where T : struct, IComparable
    {
        public override bool Equals(T x, T y)
        {
            return x.CompareTo(y) == 0;
        }
    }
}

using System;

namespace LennysWpfLibrary.Generics
{
    public class ComparableStructEqualityHelper<T> : BaseStructEqualityHelper<T>
        where T : struct, IComparable<T>
    {
        public override bool Equals(T x, T y)
        {
            return x.CompareTo(y) == 0;
        }
    }
}

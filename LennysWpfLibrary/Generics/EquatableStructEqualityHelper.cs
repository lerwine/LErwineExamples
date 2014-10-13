using System;

namespace LennysWpfLibrary.Generics
{
    public class EquatableStructEqualityHelper<T> : BaseStructEqualityHelper<T>
        where T : struct, IEquatable<T>
    {
        public override bool Equals(T x, T y)
        {
            return x.Equals(y);
        }
    }
}

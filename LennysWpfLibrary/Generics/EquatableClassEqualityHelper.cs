using System;

namespace LennysWpfLibrary.Generics
{
    public class EquatableClassEqualityHelper<T> : BaseClassEqualityHelper<T>
        where T : class, IEquatable<T>
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return x.Equals(y);
        }
    }
}

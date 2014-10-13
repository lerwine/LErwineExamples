using System;

namespace LennysWpfLibrary.Generics
{
    public class EquatableGenericEqualityHelper<T> : BaseGenericEqualityHelper<T>
        where T : IEquatable<T>
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return x.Equals(y);
        }
    }
}

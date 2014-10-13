using System.Collections.Generic;

namespace LennysWpfLibrary.Generics
{
    public class GenericEqualityHelper<T> : BaseGenericEqualityHelper<T>
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}

using System.Collections.Generic;

namespace LennysWpfLibrary.Generics
{
    public class ClassEqualityHelper<T> : BaseClassEqualityHelper<T>
        where T : class
    {
        protected override bool NonNullEquals(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}

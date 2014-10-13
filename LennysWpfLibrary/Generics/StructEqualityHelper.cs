using System.Collections.Generic;

namespace LennysWpfLibrary.Generics
{
    public class StructEqualityHelper<T> : BaseStructEqualityHelper<T>
        where T : struct
    {
        public override bool Equals(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}

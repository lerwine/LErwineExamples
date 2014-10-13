using System;

namespace LennysWpfLibrary.Generics
{
    public class EnumEqualityHelper<T> : GComparableStructEqualityHelper<T>
        where T : struct, IComparable, IFormattable, IConvertible
    {
    }
}

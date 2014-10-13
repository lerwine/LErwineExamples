using System;
using System.Linq;

namespace LennysWpfLibrary.Generics
{
    public abstract class BaseStructEqualityHelper<T> : EqualityHelper<T>
        where T : struct
    {
        private static Func<T, string> _toString = null;

        public new static BaseStructEqualityHelper<T> Create()
        {
            Type type = typeof(T);

            Type gt;

            if (type.IsEnum)
                gt = typeof(EnumEqualityHelper<>);
            else
            {
                gt = typeof(IEquatable<T>);
                if (type.GetInterfaces().Any(i => i.Equals(gt)))
                    gt = typeof(EquatableStructEqualityHelper<>);
                else
                {
                    gt = typeof(IComparable<T>);
                    if (type.GetInterfaces().Any(i => i.Equals(gt)))
                        gt = typeof(ComparableStructEqualityHelper<>);
                    else
                    {
                        gt = typeof(IComparable);
                        if (type.GetInterfaces().Any(i => i.Equals(gt)))
                            gt = typeof(GComparableStructEqualityHelper<>);
                        else
                            gt = typeof(StructEqualityHelper<>);
                    }
                }
            }

            return Activator.CreateInstance(gt.MakeGenericType(type)) as BaseStructEqualityHelper<T>;
        }

        public abstract override bool Equals(T x, T y);

        public override bool AreSame(T x, T y)
        {
            return this.Equals(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        public override string ToString(T obj)
        {
            string result;

            if (BaseStructEqualityHelper<T>._toString != null)
                return BaseStructEqualityHelper<T>._toString(obj);

            Type type = typeof(IConvertible);
            Func<T, string> toString;

            if (type.IsEnum)
            {
                toString = (T value) => Enum.GetName(value.GetType(), value);
                result = toString(obj);
            }
            else if ((typeof(T)).GetInterfaces().Any(i => i.Equals(type)))
            {
                try
                {
                    toString = (T value) => (value as IConvertible).ToString(System.Globalization.CultureInfo.CurrentCulture);
                    result = toString(obj);
                }
                catch
                {
                    toString = (T value) => value.ToString();
                    result = toString(obj);
                }
            }
            else
            {
                toString = (T value) => value.ToString();
                result = toString(obj);
            }

            BaseStructEqualityHelper<T>._toString = toString;

            return result;
        }
    }
}

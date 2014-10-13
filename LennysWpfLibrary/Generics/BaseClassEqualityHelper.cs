using System;
using System.Linq;

namespace LennysWpfLibrary.Generics
{
    public abstract class BaseClassEqualityHelper<T> : EqualityHelper<T>
        where T : class
    {
        private static Func<T, string> _toString = null;

        public new static BaseClassEqualityHelper<T> Create()
        {
            Type type = typeof(T);
            Type gt = typeof(IEquatable<T>);
            if (type.GetInterfaces().Any(i => i.Equals(gt)))
                gt = typeof(EquatableClassEqualityHelper<>);
            else
            {
                gt = typeof(IComparable<T>);
                if (type.GetInterfaces().Any(i => i.Equals(gt)))
                    gt = typeof(ComparableClassEqualityHelper<>);
                else
                {
                    gt = typeof(IComparable);
                    if (type.GetInterfaces().Any(i => i.Equals(gt)))
                        gt = typeof(GComparableClassEqualityHelper<>);
                    else
                        gt = typeof(ClassEqualityHelper<>);
                }
            }

            return Activator.CreateInstance(gt.MakeGenericType(type)) as BaseClassEqualityHelper<T>;
        }

        public override bool Equals(T x, T y)
        {
            return (x == null) ? y == null : (y != null && (Object.ReferenceEquals(x, y) || this.NonNullEquals(x, y)));
        }

        protected abstract bool NonNullEquals(T x, T y);

        public override bool AreSame(T x, T y)
        {
            return (x == null) ? y == null : (y != null && Object.ReferenceEquals(x, y));
        }

        public override int GetHashCode(T obj)
        {
            return (obj == null) ? default(int) : obj.GetHashCode();
        }

        public override string ToString(T obj)
        {
            if (obj == null)
                return "";

            string result;

            if (BaseClassEqualityHelper<T>._toString != null)
                return BaseClassEqualityHelper<T>._toString(obj);

            Type type = typeof(IConvertible);
            Func<T, string> toString;

            if ((typeof(T)).GetInterfaces().Any(i => i.Equals(type)))
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

            BaseClassEqualityHelper<T>._toString = toString;

            return result;
        }
    }
}

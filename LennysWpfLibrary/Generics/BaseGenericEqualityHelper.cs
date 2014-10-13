using System;
using System.Linq;

namespace LennysWpfLibrary.Generics
{
    public abstract class BaseGenericEqualityHelper<T> : EqualityHelper<T>
    {
        private static Func<T, string> _toString = null;

        public new static BaseGenericEqualityHelper<T> Create()
        {
            Type type = typeof(T);
            Type gt = typeof(IEquatable<T>);
            if (type.GetInterfaces().Any(i => i.Equals(gt)))
                gt = typeof(EquatableGenericEqualityHelper<>);
            else
            {
                gt = typeof(IComparable<T>);
                if (type.GetInterfaces().Any(i => i.Equals(gt)))
                    gt = typeof(ComparableGenericEqualityHelper<>);
                else
                {
                    gt = typeof(IComparable);
                    if (type.GetInterfaces().Any(i => i.Equals(gt)))
                        gt = typeof(GComparableGenericEqualityHelper<>);
                    else
                        gt = typeof(GenericEqualityHelper<>);
                }
            }

            return Activator.CreateInstance(gt.MakeGenericType(type)) as BaseGenericEqualityHelper<T>;
        }

        public override bool Equals(T x, T y)
        {
            return ((object)x == null) ? (object)y == null : ((object)y != null && (Object.ReferenceEquals(x, y) || this.NonNullEquals(x, y)));
        }

        protected abstract bool NonNullEquals(T x, T y);

        public override bool AreSame(T x, T y)
        {
            return ((object)x == null) ? (object)y == null : ((object)y != null && ((x.GetType().IsClass) ? Object.ReferenceEquals(x, y) : x.Equals(y)));
        }

        public override int GetHashCode(T obj)
        {
            return ((object)obj == null) ? default(int) : obj.GetHashCode();
        }

        public override string ToString(T obj)
        {
            if ((object)obj == null)
                return "";

            string result;

            if (BaseGenericEqualityHelper<T>._toString != null)
                return BaseGenericEqualityHelper<T>._toString(obj);


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

            BaseGenericEqualityHelper<T>._toString = toString;

            return result;
        }
    }
}

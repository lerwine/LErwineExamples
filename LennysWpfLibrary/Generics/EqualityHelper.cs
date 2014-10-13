using System;
using System.Collections.Generic;

namespace LennysWpfLibrary.Generics
{
    public abstract class EqualityHelper<T> : IEqualityComparer<T>
    {
        public static EqualityHelper<T> Create()
        {
            Type type = typeof(T);

            if (type.Equals(typeof(string)))
                return new StringEqualityHelper() as EqualityHelper<T>;

            if (type.Equals(typeof(bool)))
                return new BooleanEqualityHelper() as EqualityHelper<T>;

            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                return Activator.CreateInstance((typeof(NullableEqualityHelper<>)).MakeGenericType(Nullable.GetUnderlyingType(type))) as EqualityHelper<T>;

            Type gt;

            if (type.IsClass)
                gt = typeof(BaseClassEqualityHelper<>);
            else if (type.IsValueType)
                gt = typeof(BaseStructEqualityHelper<>);
            else
                gt = typeof(BaseGenericEqualityHelper<>);

            return gt.MakeGenericType(type).GetMethod("Create").Invoke(null, new object[0]) as EqualityHelper<T>;
        }

        public abstract bool Equals(T x, T y);
        public abstract bool AreSame(T x, T y);
        public abstract int GetHashCode(T obj);
        public abstract string ToString(T obj);
    }
}

using System;
using System.Collections.Generic;

namespace LennysWpfLibrary.Collections
{
    public class ObjectInstanceComparer<T> : IEqualityComparer<T>
        where T : class
    {
        public bool Equals(T x, T y)
        {
            return (x == null) ? (y == null) : (y != null && Object.ReferenceEquals(x, y));
        }

        public int GetHashCode(T obj)
        {
            int result;
            return (obj == null) ? int.MinValue : (((result = obj.GetHashCode()) == int.MinValue) ? int.MaxValue : result);
        }
    }
}

namespace LennysWpfLibrary.Generics
{
    public class NullableEqualityHelper<T> : EqualityHelper<T?>
        where T : struct
    {
        private static BaseStructEqualityHelper<T> _innerHelper = null;

        protected static BaseStructEqualityHelper<T> InnerHelper
        {
            get
            {
                if (NullableEqualityHelper<T>._innerHelper == null)
                    NullableEqualityHelper<T>._innerHelper = StructEqualityHelper<T>.Create();

                return NullableEqualityHelper<T>._innerHelper;
            }
        }

        public override bool Equals(T? x, T? y)
        {
            return (x.HasValue) ? (y.HasValue && NullableEqualityHelper<T>.InnerHelper.Equals(x.Value, y.Value)) : !y.HasValue;
        }

        public override bool AreSame(T? x, T? y)
        {
            return this.Equals(x, y);
        }

        public override int GetHashCode(T? obj)
        {
            return (obj.HasValue) ? NullableEqualityHelper<T>.InnerHelper.GetHashCode(obj.Value) : default(int);
        }

        public override string ToString(T? obj)
        {
            return (obj.HasValue) ? NullableEqualityHelper<T>.InnerHelper.ToString(obj.Value) : "";
        }
    }
}

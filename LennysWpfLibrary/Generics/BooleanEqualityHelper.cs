namespace LennysWpfLibrary.Generics
{
    public class BooleanEqualityHelper : EqualityHelper<bool>
    {
        public override bool Equals(bool x, bool y)
        {
            return x == y;
        }

        public override bool AreSame(bool x, bool y)
        {
            return x == y;
        }

        public override int GetHashCode(bool obj)
        {
            return obj.GetHashCode();
        }

        public override string ToString(bool obj)
        {
            return obj.ToString();
        }
    }
}

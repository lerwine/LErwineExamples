namespace LennysWpfLibrary.Generics
{
    public class StringEqualityHelper : EqualityHelper<string>
    {
        public override bool Equals(string x, string y)
        {
            return (x == null) ? (y == null) : (y != null && x == y);
        }

        public override bool AreSame(string x, string y)
        {
            return this.Equals(x, y);
        }

        public override int GetHashCode(string obj)
        {
            return (obj == null) ? default(int) : obj.GetHashCode();
        }

        public override string ToString(string obj)
        {
            return (obj == null) ? "" : obj;
        }
    }
}

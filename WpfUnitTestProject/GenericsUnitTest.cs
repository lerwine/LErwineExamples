using LennysWpfLibrary.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace WpfUnitTestProject
{
    [TestClass]
    public class GenericsUnitTest
    {
        #region Sample classes

        public interface ISampleSimple
        {
            int Property { get; }
        }

        public interface ISampleEquatable<TImplemented> : IEquatable<TImplemented>, ISampleSimple
            where TImplemented : ISampleEquatable<TImplemented>
        {

        }

        public interface IISampleEquatable : ISampleEquatable<IISampleEquatable> { }

        public interface ISampleComparable<TImplemented> : IComparable<TImplemented>, ISampleSimple
            where TImplemented : ISampleComparable<TImplemented>
        {

        }

        public interface IISampleComparable : ISampleComparable<IISampleComparable> { }

        public interface ISampleGComparable : IComparable, ISampleSimple
        {

        }

        public interface IISampleEquatableComparable : ISampleEquatable<IISampleEquatableComparable>, ISampleComparable<IISampleEquatableComparable> { }

        public class SampleSimpleClass : ISampleSimple
        {
            public int Property { get; set; }

            private bool Equals(SampleSimpleClass other)
            {
                return other != null && this.Property == other.Property;
            }

            //public override bool Equals(object obj)
            //{
            //    return this.Equals(obj as SampleSimpleClass);
            //}

            //public override int GetHashCode()
            //{
            //    return this.Property;
            //}

            public override string ToString()
            {
                return this.Property.ToString();
            }

            private int CompareTo(SampleSimpleClass other)
            {
                return (other == null) ? -1 : this.Property.CompareTo(other.Property);
            }

            private int CompareTo(object obj)
            {
                return this.CompareTo(obj as SampleSimpleClass);
            }
        }

        public class SampleEquatableClass : ISampleEquatable<SampleEquatableClass>, IISampleEquatable
        {
            public int Property { get; set; }

            public bool Equals(SampleEquatableClass other)
            {
                return other != null && this.Property == other.Property;
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as SampleEquatableClass);
            }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            private int CompareTo(SampleEquatableClass other)
            {
                return (other == null) ? -1 : this.Property.CompareTo(other.Property);
            }

            private int CompareTo(object obj)
            {
                return this.CompareTo(obj as SampleEquatableClass);
            }

            public bool Equals(IISampleEquatable other)
            {
                return this.Equals(other as SampleEquatableClass);
            }
        }

        public class SampleComparableClass : ISampleComparable<SampleComparableClass>, ISampleGComparable, IISampleComparable
        {
            public int Property { get; set; }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            public int CompareTo(SampleComparableClass other)
            {
                return (other == null) ? -1 : this.Property.CompareTo(other.Property);
            }

            public int CompareTo(object obj)
            {
                return this.CompareTo(obj as SampleComparableClass);
            }

            public int CompareTo(IISampleComparable other)
            {
                return this.CompareTo(other as SampleComparableClass);
            }
        }

        public class SampleGComparableClass : ISampleGComparable
        {
            public int Property { get; set; }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            private int CompareTo(SampleGComparableClass other)
            {
                return (other == null) ? -1 : this.Property.CompareTo(other.Property);
            }

            public int CompareTo(object obj)
            {
                return this.CompareTo(obj as SampleGComparableClass);
            }
        }

        public class SampleEquatableComparableClass : ISampleEquatable<SampleEquatableComparableClass>, ISampleComparable<SampleEquatableComparableClass>, ISampleGComparable, IISampleEquatable, IISampleComparable
        {
            public int Property { get; set; }

            public bool Equals(SampleEquatableComparableClass other)
            {
                return other != null && this.Property == other.Property;
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as SampleEquatableComparableClass);
            }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            public int CompareTo(SampleEquatableComparableClass other)
            {
                return (other == null) ? -1 : this.Property.CompareTo(other.Property);
            }

            public int CompareTo(object obj)
            {
                return this.CompareTo(obj as SampleEquatableComparableClass);
            }

            public bool Equals(IISampleEquatable other)
            {
                return this.Equals(other as SampleEquatableComparableClass);
            }

            public int CompareTo(IISampleComparable other)
            {
                return this.CompareTo(other as SampleEquatableComparableClass);
            }
        }

        public struct SampleSimpleStruct : ISampleSimple
        {
            private int _field;
            public int Property { get { return this._field; } }

            public SampleSimpleStruct(int value)
            {
                this._field = value;
            }

            //public bool Equals(SampleSimpleStruct other)
            //{
            //    return this.Property == other.Property;
            //}

            //public override bool Equals(object obj)
            //{
            //    return obj != null && obj is SampleSimpleStruct && this.Equals((SampleSimpleStruct)obj);
            //}

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            private int CompareTo(SampleSimpleStruct other)
            {
                return this.Property.CompareTo(other.Property);
            }

            private int CompareTo(object obj)
            {
                return (obj != null && obj is SampleSimpleStruct) ? this.Property.CompareTo(((SampleSimpleStruct)obj).Property) : -1;
            }
        }

        public struct SampleEquatableStruct : ISampleEquatable<SampleEquatableStruct>, IISampleEquatable
        {
            private int _field;
            public int Property { get { return this._field; } }

            public SampleEquatableStruct(int value)
            {
                this._field = value;
            }

            public bool Equals(SampleEquatableStruct other)
            {
                return this.Property == other.Property;
            }

            public override bool Equals(object obj)
            {
                return obj != null && obj is SampleEquatableStruct && this.Equals((SampleEquatableStruct)obj);
            }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            private int CompareTo(SampleEquatableStruct other)
            {
                return this.Property.CompareTo(other.Property);
            }

            private int CompareTo(object obj)
            {
                return (obj != null && obj is SampleEquatableStruct) ? this.Property.CompareTo(((SampleEquatableStruct)obj).Property) : -1;
            }

            public bool Equals(IISampleEquatable other)
            {
                return other != null && other is SampleEquatableStruct && this.Equals((SampleEquatableStruct)other);
            }
        }

        public struct SampleComparableStruct : ISampleComparable<SampleComparableStruct>, ISampleGComparable, IISampleComparable
        {
            private int _field;
            public int Property { get { return this._field; } }

            public SampleComparableStruct(int value)
            {
                this._field = value;
            }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            public int CompareTo(SampleComparableStruct other)
            {
                return this.Property.CompareTo(other.Property);
            }

            public int CompareTo(object obj)
            {
                return (obj != null && obj is SampleComparableStruct) ? this.Property.CompareTo(((SampleComparableStruct)obj).Property) : -1;
            }

            public int CompareTo(IISampleComparable other)
            {
                return (other != null && other is SampleComparableStruct) ? this.Property.CompareTo(((SampleComparableStruct)other).Property) : -1;
            }
        }

        public struct SampleGComparableStruct : ISampleGComparable
        {
            private int _field;
            public int Property { get { return this._field; } }

            public SampleGComparableStruct(int value)
            {
                this._field = value;
            }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            private int CompareTo(SampleGComparableStruct other)
            {
                return this.Property.CompareTo(other.Property);
            }

            public int CompareTo(object obj)
            {
                return (obj != null && obj is SampleGComparableStruct) ? this.Property.CompareTo(((SampleGComparableStruct)obj).Property) : -1;
            }
        }

        public struct SampleEquatableComparableStruct : ISampleEquatable<SampleEquatableComparableStruct>, ISampleComparable<SampleEquatableComparableStruct>, ISampleGComparable, IISampleEquatable, IISampleComparable
        {
            private int _field;
            public int Property { get { return this._field; } }

            public SampleEquatableComparableStruct(int value)
            {
                this._field = value;
            }

            public bool Equals(SampleEquatableComparableStruct other)
            {
                return this.Property == other.Property;
            }

            public override bool Equals(object obj)
            {
                return obj != null && obj is SampleEquatableComparableStruct && this.Equals((SampleEquatableComparableStruct)obj);
            }

            public override int GetHashCode()
            {
                return this.Property;
            }

            public override string ToString()
            {
                return this.Property.ToString();
            }

            public int CompareTo(SampleEquatableComparableStruct other)
            {
                return this.Property.CompareTo(other.Property);
            }

            public int CompareTo(object obj)
            {
                return (obj != null && obj is SampleEquatableComparableStruct) ? this.Property.CompareTo(((SampleEquatableComparableStruct)obj).Property) : -1;
            }

            public bool Equals(IISampleEquatable other)
            {
                return other != null && other is SampleEquatableComparableStruct && this.Equals((SampleEquatableComparableStruct)other);
            }

            public int CompareTo(IISampleComparable other)
            {
                return (other != null && other is SampleEquatableComparableStruct) ? this.Property.CompareTo(((SampleEquatableComparableStruct)other).Property) : -1;
            }
        }

        public enum SampleEnum
        {
            NegativeOne = -1,
            One = 1,
            Two = 2,
            Three = 3
        }

        #endregion

        [TestMethod]
        public void EqualityHelperTestMethod()
        {
            object helper = EqualityHelper<string>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(StringEqualityHelper));

            helper = EqualityHelper<bool>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(BooleanEqualityHelper));

            helper = EqualityHelper<int?>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(NullableEqualityHelper<int>));

            helper = EqualityHelper<bool?>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(NullableEqualityHelper<bool>));

            helper = EqualityHelper<bool?>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(NullableEqualityHelper<bool>));

            helper = EqualityHelper<SampleSimpleStruct>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(BaseStructEqualityHelper<SampleSimpleStruct>));

            helper = EqualityHelper<SampleSimpleClass>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(BaseClassEqualityHelper<SampleSimpleClass>));

            helper = EqualityHelper<ISampleSimple>.Create();
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(BaseGenericEqualityHelper<ISampleSimple>));
        }

        [TestMethod]
        public void BooleanEqualityHelperTestMethod1()
        {
            BooleanEqualityHelper helper = new BooleanEqualityHelper();
            bool expected = true;
            bool x = true;
            bool y = true;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = x.GetHashCode();
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = x.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = false;
            y = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = x.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = false;
            x = true;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = false;
            y = true;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BooleanEqualityHelperTestMethod2()
        {
            EqualityComparer<bool> helper = EqualityComparer<bool>.Default;
            bool expected = true;
            bool x = true;
            bool y = true;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = x.GetHashCode();
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = false;
            y = false;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            expected = false;
            x = true;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = false;
            y = true;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringEqualityHelperTestMethod1()
        {
            StringEqualityHelper helper = new StringEqualityHelper();
            bool expected = true;
            string x = null;
            string y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = default(int);
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = "";
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);


            x = "";
            y = "";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = x;
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = " ";
            y = " ";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = x;
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = "test";
            y = "test";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = x;
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = false;
            x = null;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            y = "";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = " ";
            y = null;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = "";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            y = " ";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = "test1";
            y = "test2";
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringEqualityHelperTestMethod2()
        {
            EqualityComparer<string> helper = EqualityComparer<string>.Default;
            bool expected = true;
            string x = null;
            string y = null;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = default(int);
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = "";
            y = "";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = " ";
            y = " ";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = "test";
            y = "test";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            expected = false;
            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            y = "";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = " ";
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = "";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            y = " ";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = "test1";
            y = "test2";
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EquatableClassEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleEquatableClass> obj = EqualityHelper<SampleEquatableClass>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(EquatableClassEqualityHelper<SampleEquatableClass>));
            EquatableClassEqualityHelper<SampleEquatableClass> helper = new EquatableClassEqualityHelper<SampleEquatableClass>();
            bool expected = true;
            SampleEquatableClass x = null;
            SampleEquatableClass y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            
            int property = 7;
            x = new SampleEquatableClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleEquatableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void EquatableClassEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleEquatableClass> helper = EqualityComparer<SampleEquatableClass>.Default;
            bool expected = true;
            SampleEquatableClass x = null;
            SampleEquatableClass y = null;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleEquatableClass { Property = property };
            y = x;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleEquatableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void ComparableClassEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleComparableClass> obj = EqualityHelper<SampleComparableClass>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(ComparableClassEqualityHelper<SampleComparableClass>));
            ComparableClassEqualityHelper<SampleComparableClass> helper = new ComparableClassEqualityHelper<SampleComparableClass>();
            bool expected = true;
            SampleComparableClass x = null;
            SampleComparableClass y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleComparableClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleComparableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        //[TestMethod]
        //public void ComparableClassEqualityHelperTestMethod2()
        //{
        //    EqualityComparer<SampleComparableClass> helper = EqualityComparer<SampleComparableClass>.Default;
        //    bool expected = true;
        //    SampleComparableClass x = null;
        //    SampleComparableClass y = null;
        //    bool actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    int property = 7;
        //    x = new SampleComparableClass { Property = property };
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    y = new SampleComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    int expectedInt = property;
        //    int actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleComparableClass { Property = property };
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);
        //}

        [TestMethod]
        public void GComparableClassEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleGComparableClass> obj = EqualityHelper<SampleGComparableClass>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(GComparableClassEqualityHelper<SampleGComparableClass>));
            GComparableClassEqualityHelper<SampleGComparableClass> helper = new GComparableClassEqualityHelper<SampleGComparableClass>();
            bool expected = true;
            SampleGComparableClass x = null;
            SampleGComparableClass y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleGComparableClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleGComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleGComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleGComparableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        //[TestMethod]
        //public void GComparableClassEqualityHelperTestMethod2()
        //{
        //    EqualityComparer<SampleGComparableClass> helper = EqualityComparer<SampleGComparableClass>.Default;
        //    bool expected = true;
        //    SampleGComparableClass x = null;
        //    SampleGComparableClass y = null;
        //    bool actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    int property = 7;
        //    x = new SampleGComparableClass { Property = property };
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    y = new SampleGComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    int expectedInt = property;
        //    int actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleGComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleGComparableClass { Property = property };
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);
        //}

        [TestMethod]
        public void ClassEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleSimpleClass> obj = EqualityHelper<SampleSimpleClass>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(ClassEqualityHelper<SampleSimpleClass>));
            ClassEqualityHelper<SampleSimpleClass> helper = new ClassEqualityHelper<SampleSimpleClass>();
            bool expected = true;
            SampleSimpleClass x = null;
            SampleSimpleClass y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleSimpleClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            expected = false;
            y = new SampleSimpleClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = x.GetHashCode();
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleSimpleClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleSimpleClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ClassEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleSimpleClass> helper = EqualityComparer<SampleSimpleClass>.Default;
            bool expected = true;
            SampleSimpleClass x = null;
            SampleSimpleClass y = null;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleSimpleClass { Property = property };
            y = x;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            expected = false;
            y = new SampleSimpleClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = x.GetHashCode();
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            x = new SampleSimpleClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            property = 0;
            x = new SampleSimpleClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EquatableStructEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleEquatableStruct> obj = EqualityHelper<SampleEquatableStruct>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(EquatableStructEqualityHelper<SampleEquatableStruct>));
            EquatableStructEqualityHelper<SampleEquatableStruct> helper = new EquatableStructEqualityHelper<SampleEquatableStruct>();
            bool expected = true;
            int property = 7;
            SampleEquatableStruct x = new SampleEquatableStruct(property);
            SampleEquatableStruct y = x;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleEquatableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleEquatableStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void EquatableStructEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleEquatableStruct> helper = EqualityComparer<SampleEquatableStruct>.Default;
            bool expected = true;
            int property = 7;
            SampleEquatableStruct x = new SampleEquatableStruct(property);
            SampleEquatableStruct y = x;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleEquatableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleEquatableStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void ComparableStructEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleComparableStruct> obj = EqualityHelper<SampleComparableStruct>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(ComparableStructEqualityHelper<SampleComparableStruct>));
            ComparableStructEqualityHelper<SampleComparableStruct> helper = new ComparableStructEqualityHelper<SampleComparableStruct>();
            bool expected = true;
            int property = 7;
            SampleComparableStruct x = new SampleComparableStruct(property);
            SampleComparableStruct y = x;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleComparableStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }
        
        [TestMethod]
        public void ComparableStructEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleComparableStruct> helper = EqualityComparer<SampleComparableStruct>.Default;
            bool expected = true;
            int property = 7;
            SampleComparableStruct x = new SampleComparableStruct(property);
            SampleComparableStruct y = x;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleComparableStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void GComparableStructEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleGComparableStruct> obj = EqualityHelper<SampleGComparableStruct>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(GComparableStructEqualityHelper<SampleGComparableStruct>));
            GComparableStructEqualityHelper<SampleGComparableStruct> helper = new GComparableStructEqualityHelper<SampleGComparableStruct>();
            bool expected = true;
            int property = 7;
            SampleGComparableStruct x = new SampleGComparableStruct(property);
            SampleGComparableStruct y = x;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleGComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleGComparableStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void GComparableStructEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleGComparableStruct> helper = EqualityComparer<SampleGComparableStruct>.Default;
            bool expected = true;
            int property = 7;
            SampleGComparableStruct x = new SampleGComparableStruct(property);
            SampleGComparableStruct y = x;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleGComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleGComparableStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void EnumEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleEnum> obj = EqualityHelper<SampleEnum>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(EnumEqualityHelper<SampleEnum>));
            EnumEqualityHelper<SampleEnum> helper = new EnumEqualityHelper<SampleEnum>();
            bool expected = true;
            SampleEnum x = SampleEnum.One;
            SampleEnum y = SampleEnum.One;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = (int)x;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = Enum.GetName(x.GetType(), x);
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = false;
            x = SampleEnum.NegativeOne;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = (int)x;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = Enum.GetName(x.GetType(), x);
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = SampleEnum.Two;
            expectedInt = (int)x;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = Enum.GetName(x.GetType(), x);
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void EnumEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleEnum> helper = EqualityComparer<SampleEnum>.Default;
            bool expected = true;
            SampleEnum x = SampleEnum.One;
            SampleEnum y = SampleEnum.One;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = (int)x;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            expected = false;
            x = SampleEnum.NegativeOne;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = (int)x;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = SampleEnum.Two;
            expectedInt = (int)x;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void StructEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleSimpleStruct> obj = EqualityHelper<SampleSimpleStruct>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(StructEqualityHelper<SampleSimpleStruct>));
            StructEqualityHelper<SampleSimpleStruct> helper = new StructEqualityHelper<SampleSimpleStruct>();
            bool expected = true;
            int property = 7;
            SampleSimpleStruct x = new SampleSimpleStruct(property);
            SampleSimpleStruct y = x;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleSimpleStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleSimpleStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void StructEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleSimpleStruct> helper = EqualityComparer<SampleSimpleStruct>.Default;
            bool expected = true;
            int property = 7;
            SampleSimpleStruct x = new SampleSimpleStruct(property);
            SampleSimpleStruct y = x;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleSimpleStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleSimpleStruct(property);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void NullableEqualityHelperTestMethod1()
        {
            EqualityHelper<SampleSimpleStruct?> obj = EqualityHelper<SampleSimpleStruct?>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(NullableEqualityHelper<SampleSimpleStruct>));
            NullableEqualityHelper<SampleSimpleStruct> helper = new NullableEqualityHelper<SampleSimpleStruct>();
            bool expected = true;
            SampleSimpleStruct? x = null;
            SampleSimpleStruct? y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleSimpleStruct(property);
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleSimpleStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleSimpleStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void NullableEqualityHelperTestMethod2()
        {
            EqualityComparer<SampleSimpleStruct?> helper = EqualityComparer<SampleSimpleStruct?>.Default;
            bool expected = true;
            SampleSimpleStruct? x = null;
            SampleSimpleStruct? y = null;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleSimpleStruct(property);
            y = x;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleSimpleStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleSimpleStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void EquatableGenericEqualityHelperTestMethod1()
        {
            EqualityHelper<IISampleEquatable> obj = EqualityHelper<IISampleEquatable>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(EquatableGenericEqualityHelper<IISampleEquatable>));
            EquatableGenericEqualityHelper<IISampleEquatable> helper = new EquatableGenericEqualityHelper<IISampleEquatable>();
            bool expected = true;
            IISampleEquatable x = null;
            IISampleEquatable y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleEquatableClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleEquatableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = true;
            property = 7;
            x = new SampleEquatableStruct(property);
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleEquatableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = new SampleEquatableClass { Property = property };
            y = new SampleEquatableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleEquatableStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void EquatableGenericEqualityHelperTestMethod2()
        {
            EqualityComparer<IISampleEquatable> helper = EqualityComparer<IISampleEquatable>.Default;
            bool expected = true;
            IISampleEquatable x = null;
            IISampleEquatable y = null;
            bool actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleEquatableClass { Property = property };
            y = x;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleEquatableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleEquatableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            expected = true;
            property = 7;
            x = new SampleEquatableStruct(property);
            y = x;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property++;
            expected = false;
            x = new SampleEquatableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            x = new SampleEquatableClass { Property = property };
            y = new SampleEquatableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);

            property = 0;
            x = new SampleEquatableStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void ComparableGenericEqualityHelperTestMethod1()
        {
            EqualityHelper<IISampleComparable> obj = EqualityHelper<IISampleComparable>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(ComparableGenericEqualityHelper<IISampleComparable>));
            ComparableGenericEqualityHelper<IISampleComparable> helper = new ComparableGenericEqualityHelper<IISampleComparable>();
            bool expected = true;
            IISampleComparable x = null;
            IISampleComparable y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleComparableClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleComparableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = true;
            property = 7;
            x = new SampleComparableStruct(property);
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = new SampleComparableClass { Property = property };
            y = new SampleComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleComparableStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        //[TestMethod]
        //public void ComparableGenericEqualityHelperTestMethod2()
        //{
        //    EqualityComparer<IISampleComparable> helper = EqualityComparer<IISampleComparable>.Default;
        //    bool expected = true;
        //    IISampleComparable x = null;
        //    IISampleComparable y = null;
        //    bool actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    int property = 7;
        //    x = new SampleComparableClass { Property = property };
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    y = new SampleComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    int expectedInt = property;
        //    int actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleComparableClass { Property = property };
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    expected = true;
        //    property = 7;
        //    x = new SampleComparableStruct(property);
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleComparableStruct(property);
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = new SampleComparableClass { Property = property };
        //    y = new SampleComparableStruct(property);
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleComparableStruct(property);
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);
        //}

        [TestMethod]
        public void GComparableGenericEqualityHelperTestMethod1()
        {
            EqualityHelper<ISampleGComparable> obj = EqualityHelper<ISampleGComparable>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(GComparableGenericEqualityHelper<ISampleGComparable>));
            GComparableGenericEqualityHelper<ISampleGComparable> helper = new GComparableGenericEqualityHelper<ISampleGComparable>();
            bool expected = true;
            ISampleGComparable x = null;
            ISampleGComparable y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);

            int property = 7;
            x = new SampleGComparableClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            y = new SampleGComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = property;
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleGComparableClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleGComparableClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = true;
            property = 7;
            x = new SampleGComparableStruct(property);
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleGComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = new SampleGComparableClass { Property = property };
            y = new SampleGComparableStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = default(int);
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleGComparableStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = property;
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        //[TestMethod]
        //public void GComparableGenericEqualityHelperTestMethod2()
        //{
        //    EqualityComparer<ISampleGComparable> helper = EqualityComparer<ISampleGComparable>.Default;
        //    bool expected = true;
        //    ISampleGComparable x = null;
        //    ISampleGComparable y = null;
        //    bool actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    int property = 7;
        //    x = new SampleGComparableClass { Property = property };
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    y = new SampleGComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    int expectedInt = property;
        //    int actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleGComparableClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleGComparableClass { Property = property };
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    expected = true;
        //    property = 7;
        //    x = new SampleGComparableStruct(property);
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleGComparableStruct(property);
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = new SampleGComparableClass { Property = property };
        //    y = new SampleGComparableStruct(property);
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleGComparableStruct(property);
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);
        //}

        [TestMethod]
        public void GenericEqualityHelperTestMethod1()
        {
            EqualityHelper<ISampleSimple> obj = EqualityHelper<ISampleSimple>.Create();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(GenericEqualityHelper<ISampleSimple>));
            GenericEqualityHelper<ISampleSimple> helper = new GenericEqualityHelper<ISampleSimple>();
            bool expected = true;
            ISampleSimple x = null;
            ISampleSimple y = null;
            bool actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            int property = 7;
            x = new SampleSimpleClass { Property = property };
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);

            expected = false;
            y = new SampleSimpleClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            int expectedInt = x.GetHashCode();
            int actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            string expectedString = property.ToString();
            string actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            x = new SampleSimpleClass { Property = property };
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleSimpleClass { Property = property };
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            expected = true;
            property = 7;
            x = new SampleSimpleStruct(property);
            y = x;
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            expectedInt = x.GetHashCode();
            actualInt = helper.GetHashCode(x);
            Assert.AreEqual(expectedInt, actualInt);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property++;
            expected = false;
            x = new SampleSimpleStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            x = new SampleSimpleClass { Property = property };
            y = new SampleSimpleStruct(property);
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);

            x = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = "";
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);

            property = 0;
            x = new SampleSimpleStruct(property);
            y = null;
            actual = helper.Equals(x, y);
            Assert.AreEqual(expected, actual);
            actual = helper.AreSame(x, y);
            Assert.AreEqual(expected, actual);
            expectedString = property.ToString();
            actualString = helper.ToString(x);
            Assert.AreEqual(expectedString, actualString);
        }

        //[TestMethod]
        //public void GenericEqualityHelperTestMethod2()
        //{
        //    EqualityComparer<ISampleSimple> helper = EqualityComparer<ISampleSimple>.Default;
        //    bool expected = true;
        //    ISampleSimple x = null;
        //    ISampleSimple y = null;
        //    bool actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    int property = 7;
        //    x = new SampleSimpleClass { Property = property };
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    y = new SampleSimpleClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    int expectedInt = property;
        //    int actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleSimpleClass { Property = property };
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleSimpleClass { Property = property };
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    expected = true;
        //    property = 7;
        //    x = new SampleSimpleStruct(property);
        //    y = x;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property++;
        //    expected = false;
        //    x = new SampleSimpleStruct(property);
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    x = new SampleSimpleClass { Property = property };
        //    y = new SampleSimpleStruct(property);
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);

        //    x = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = default(int);
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);

        //    property = 0;
        //    x = new SampleSimpleStruct(property);
        //    y = null;
        //    actual = helper.Equals(x, y);
        //    Assert.AreEqual(expected, actual);
        //    expectedInt = property;
        //    actualInt = helper.GetHashCode(x);
        //    Assert.AreEqual(expectedInt, actualInt);
        //}
    }
}

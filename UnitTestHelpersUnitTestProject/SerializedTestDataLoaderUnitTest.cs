using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using UnitTestHelpers;
using System.Xml;
using System.Linq;

namespace UnitTestHelpersUnitTestProject
{
    [TestClass]
    public class SerializedTestDataLoaderUnitTest
    {
        [TestMethod]
        public void CreateDefaultSerializerTestMethod()
        {
            SerializedTestDataLoader<ExampleTestData1>.CreateDefaultSerializer();
        }

        [TestMethod]
        public void CreateDefaultReaderSettingsTestMethod1()
        {
            XmlReaderSettings target = SerializedTestDataLoader<ExampleTestData1>.CreateDefaultReaderSettings();
            bool expected = false;
            bool actual = target.CloseInput;
            Assert.AreEqual(expected, actual);
            actual = target.Async;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDefaultReaderSettingsTestMethod2()
        {
            bool closeInput = false;
            XmlReaderSettings target = SerializedTestDataLoader<ExampleTestData1>.CreateDefaultReaderSettings(closeInput);
            bool expected = false;
            bool actual = target.CloseInput;
            Assert.AreEqual(expected, actual);
            actual = target.Async;
            Assert.AreEqual(expected, actual);

            closeInput = true;
            target = SerializedTestDataLoader<ExampleTestData1>.CreateDefaultReaderSettings(closeInput);
            expected = true;
            actual = target.CloseInput;
            Assert.AreEqual(expected, actual);
            actual = target.Async;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDefaultWriterSettingsTestMethod1()
        {
            XmlWriterSettings target = SerializedTestDataLoader<ExampleTestData1>.CreateDefaultWriterSettings();
            bool expected = false;
            bool actual = target.CloseOutput;
            Assert.AreEqual(expected, actual);
            actual = target.Async;
            Assert.AreEqual(expected, actual);
            expected = true;
            actual = target.WriteEndDocumentOnClose;
            Assert.AreEqual(expected, actual);
            actual = target.Indent;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(target.Encoding);
            string expectedEncoding = System.Text.Encoding.UTF8.EncodingName;
            string actualEncoding = target.Encoding.EncodingName;
            Assert.AreEqual(expectedEncoding, actualEncoding);
        }

        [TestMethod]
        public void CreateDefaultWriterSettingsTestMethod2()
        {
            bool closeOutput = false;
            XmlWriterSettings target = SerializedTestDataLoader<ExampleTestData1>.CreateDefaultWriterSettings(closeOutput);
            bool expected = false;
            bool actual = target.CloseOutput;
            Assert.AreEqual(expected, actual);
            actual = target.Async;
            Assert.AreEqual(expected, actual);
            expected = true;
            actual = target.WriteEndDocumentOnClose;
            Assert.AreEqual(expected, actual);
            actual = target.Indent;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(target.Encoding);
            string expectedEncoding = System.Text.Encoding.UTF8.EncodingName;
            string actualEncoding = target.Encoding.EncodingName;
            Assert.AreEqual(expectedEncoding, actualEncoding);

            closeOutput = true;
            target = SerializedTestDataLoader<ExampleTestData1>.CreateDefaultWriterSettings(closeOutput);
            actual = target.CloseOutput;
            Assert.AreEqual(expected, actual);
            actual = target.Async;
            Assert.AreEqual(expected, actual);
            actual = target.WriteEndDocumentOnClose;
            Assert.AreEqual(expected, actual);
            actual = target.Indent;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(target.Encoding);
            expectedEncoding = System.Text.Encoding.UTF8.EncodingName;
            actualEncoding = target.Encoding.EncodingName;
            Assert.AreEqual(expectedEncoding, actualEncoding);
        }

        [TestMethod]
        public void SaveTestMethod1()
        {
            ExampleTestData1 target = new ExampleTestData1
            {
                 Values = new object[]
                {
                    true,
                    false,
                    TypeCode.Decimal,
                    (byte)128,
                    (sbyte)12,
                    'x',
                    (short)1,
                    (ushort)2,
                    (int)3,
                    (uint)4,
                    (long)5,
                    (ulong)6,
                    DateTime.Now,
                    (decimal)6.5,
                    (double)12.5,
                    (float)1500.3,
                    "testing",
                    null,
                    (new bool[] { }).Cast<object>().ToArray(),
                    (new bool[] { true, false }).Cast<object>().ToArray(),
                    (new TypeCode[] { TypeCode.Double, TypeCode.Empty, TypeCode.Int16 }).Cast<object>().ToArray(),
                    (new byte[] { 1, 23, 45 }).Cast<object>().ToArray(),
                    (new sbyte[] { 6, 3, 8, 3 }).Cast<object>().ToArray(),
                    (new char[] { 'a', 'b', 'c', 'd', 'e' }).Cast<object>().ToArray(),
                    (new short[] { 4, 5, 7, 7 }).Cast<object>().ToArray(),
                    (new ushort[] { 0, 0, 0, 0 }).Cast<object>().ToArray(),
                    (new int[] { 88, 55, 22 }).Cast<object>().ToArray(),
                    (new uint[] { 543535, 4432 }).Cast<object>().ToArray(),
                    (new long[] { 88 }).Cast<object>().ToArray(),
                    (new ulong[] { 8348538853 }).Cast<object>().ToArray(),
                    (new DateTime[] { DateTime.MinValue, DateTime.MaxValue, DateTime.UtcNow.AddDays(12) }).Cast<object>().ToArray(),
                    (new decimal[] { 2.3m, 4.5m, 6.7m }).Cast<object>().ToArray(),
                    (new double[] { 77, 66, 55, 44, 33, 22, 11 }).Cast<object>().ToArray(),
                    (new float[] { 5.67f, 9.87f }).Cast<object>().ToArray(),
                    (new string[] { "This", "is", "the", "one" }).Cast<object>().ToArray(),
                    (new bool?[] { true, null, false }).Cast<object>().ToArray(),
                    (new TypeCode?[] { TypeCode.Double, null, TypeCode.Empty, TypeCode.Int16 }).Cast<object>().ToArray()
                },
                 NullableArray = new bool?[] { true, null, false },
                 StringArray = new string[] { "yes", null, "no" }
            };

            SerializedTestDataLoader<ExampleTestData1>.Save(target, @"..\..\MyOutput.xml", true);
        }
    }
}

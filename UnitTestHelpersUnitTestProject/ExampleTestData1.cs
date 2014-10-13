using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnitTestHelpers;

namespace UnitTestHelpersUnitTestProject
{
    [XmlRoot(Namespace="urn:test2.com")]
    public class ExampleTestData1 : TestData
    {
        public object[] Values { get; set; }
        public bool?[] NullableArray { get; set; }
        public string[] StringArray { get; set; }
    }
}

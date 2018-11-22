using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Utils.Class;
using Kopigi.Utils.Helpers;
using NFluent;
using NUnit.Framework;

namespace Kopigi.Utils.Tests.Helpers
{
    public class StringEnumShould
    {
        [TestCase(StringEnumAttributeTest.Data, "Data")]
        [TestCase(StringEnumAttributeTest.Handle, "Handle")]
        [TestCase(StringEnumAttributeTest.Short, "Short")]
        public void Return_Data_when_enum_value_is_Data(StringEnumAttributeTest data, string expected)
        {
            Check.That(StringEnum.GetStringValue(data)).IsEqualTo(expected);
        }

        [Test]
        public void Return_Data_when_enum_value_is_Data_and_when_values_is_on_cache()
        {
            Check.That(StringEnum.GetStringValue(StringEnumAttributeTest.Data)).IsEqualTo("Data");
        }

        [Test]
        public void Return_string_empty_when_enum_value_have_not_string_enum_attribute()
        {
            Check.That(StringEnum.GetStringValue(StringEnumAttributeTest.NoValue)).IsEqualTo("");
        }
    }

    public enum StringEnumAttributeTest
    {
        [StringEnum("Data")]
        Data,
        [StringEnum("Handle")]
        Handle,
        [StringEnum("Short")]
        Short,
        NoValue
    }
}

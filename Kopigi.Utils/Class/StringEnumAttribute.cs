using System;

namespace Kopigi.Utils.Class
{
    public class StringEnumAttribute : Attribute
    {
        public string Value { get; }

        public StringEnumAttribute(string value)
        {
            Value = value;
        }
    }
}

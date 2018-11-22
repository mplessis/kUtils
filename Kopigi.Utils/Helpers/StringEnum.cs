using System;
using System.Collections.Generic;
using Kopigi.Utils.Class;

namespace Kopigi.Utils.Helpers
{
    /// <summary>
    /// Gestion des valeurs String pour l'attibut StringEnumAttribute
    /// </summary>
    /// <remarks>Inspiré par http://www.codeproject.com/Articles/11130/String-Enumerations-in-C</remarks>
    public static class StringEnum
    {
        private static readonly IDictionary<Enum, StringEnumAttribute> StringValues = new Dictionary<Enum, StringEnumAttribute>();

        /// <summary>
        /// Permet de retrouver la valeur string d'un enum marqué par StringEnumAttribute
        /// </summary>
        /// <param name="value">l'enum voulu</param>
        /// <returns>la valeur String associé ou null si non trouvé</returns>
        public static string GetStringValue(Enum value)
        {
            var output = StringValues.ContainsKey(value) ? GetValueInCache(value) : ReadValueOnAttribute(value, value.GetType());
            return output;
        }

        private static string ReadValueOnAttribute(Enum value, Type type)
        {
            var output= string.Empty;
            var fi = type.GetField(value.ToString());
            if (fi.GetCustomAttributes(typeof(StringEnumAttribute), false) is StringEnumAttribute[] attrs && attrs.Length > 0)
            {
                StringValues.Add(value, attrs[0]);
                output = attrs[0].Value;
            }
            return output;
        }

        private static string GetValueInCache(Enum value)
        {
            var output= string.Empty;
            if (StringValues[value] is StringEnumAttribute stringEnumAttribute)
            {
                output = stringEnumAttribute.Value;
            }

            return output;
        }
    }
}

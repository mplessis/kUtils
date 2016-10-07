using Kopigi.Portable.Class;
using System;
using System.Collections;

namespace Kopigi.Net45.Helpers
{
    /// <summary>
    /// Gestion des valeurs String pour l'attibut StringEnumAttribute
    /// </summary>
    /// <remarks>Inspiré par http://www.codeproject.com/Articles/11130/String-Enumerations-in-C</remarks>
    public static class StringEnumHelper
    {
        private static readonly Hashtable StringValues = new Hashtable();

        /// <summary>
        /// Permet de retrouver la valeur string d'un enum marqué par StringEnumAttribute
        /// </summary>
        /// <param name="value">l'enum voulu</param>
        /// <returns>la valeur String associé ou null si non trouvé</returns>
        public static string GetStringValue(Enum value)
        {
            string output = null;
            var type = value.GetType();
            
            if (StringValues.ContainsKey(value))
            {
                var stringEnumAttribute = StringValues[value] as StringEnumAttribute;
                if (stringEnumAttribute != null)
                    output = stringEnumAttribute.Value;
            }
            else
            {
                var fi = type.GetField(value.ToString());
                var attrs = fi.GetCustomAttributes(typeof(StringEnumAttribute), false) as StringEnumAttribute[];
                if (attrs.Length > 0)
                {
                    StringValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }

            }
            return output;
        }
    }
}

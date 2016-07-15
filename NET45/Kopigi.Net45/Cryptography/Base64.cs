using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Net45.Cryptography
{
    /// <summary>
    /// Permet de gérer un hachage au format Base64
    /// </summary>
    public static class Base64
    {
        /// <summary>
        /// Encode en base 64 pour masquer la valeur
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Décode la valeur en base 64
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decode(string value)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}

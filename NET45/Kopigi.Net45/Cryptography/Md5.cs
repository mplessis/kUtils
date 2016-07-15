using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Net45.Cryptography
{
    /// <summary>
    /// Permet de gérer un hachage au format MD5
    /// </summary>
    public static class Md5
    {
        /// <summary>
        /// Vérifie la valeur demandée par rapport à une autre
        /// </summary>
        /// <param name="inputToHash">Valeur demandée</param>
        /// <param name="checkedValue">Valeur voulue au format MD5</param>
        /// <returns><c>true</c> si ok, sinon <c>false</c></returns>
        public static bool CheckMd5(string inputToHash, string checkedValue)
        {
            var md5 = HashMd5(inputToHash);
            return string.Equals(md5, checkedValue);
        }

        /// <summary>
        /// Encode la valeur au format MD5
        /// </summary>
        /// <param name="input">Valeur à encoder</param>
        /// <returns>La valeur encodée</returns>
        public static string HashMd5(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var k in hash)
            {
                sb.Append(k.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Text;

namespace Kopigi.Net45.Cryptography
{
    public static class Sha256
    {
        /// <summary>
        /// Encode la valeur en chiffrant avec un algorythme SHA256
        /// </summary>
        /// <param name="input">Valeur à encoder</param>
        /// <returns>La valeur encodée</returns>
        public static string HashValue(string input)
        {
            var mHash = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = mHash.ComputeHash(byteValue);
            mHash.Clear();
            return BitConverter.ToString(byteHash).Replace("-", string.Empty).ToLower();
        }
    }
}

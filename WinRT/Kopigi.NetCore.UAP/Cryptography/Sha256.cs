using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace Kopigi.NetCore.UAP.Cryptography
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
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            var mHash = alg.CreateHash();
            var byteValue = Encoding.UTF8.GetBytes(input);
            mHash.Append(byteValue.AsBuffer());
            return BitConverter.ToString(mHash.GetValueAndReset().ToArray()).Replace("-", string.Empty).ToLower();
        }
    }
}

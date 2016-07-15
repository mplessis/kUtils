using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace Kopigi.NetCore.UAP.Cryptography
{
    /// <summary>
    /// Gestion du MD5
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
            var res = HashMd5(inputToHash);
            return string.Equals(res, checkedValue);
        }

        /// <summary>
        /// Vérifie la valeur demandée par rapport à une autre
        /// </summary>
        /// <param name="inputToHash">Valeur demandée</param>
        /// <param name="checkedValue">Valeur voulue au format MD5</param>
        /// <returns><c>true</c> si ok, sinon <c>false</c></returns>
        public static bool CheckMd5(byte[] inputToHash, byte[] checkedValue)
        {
            var res = HashMd5(inputToHash);
            return byte.Equals(res, checkedValue);
        }

        /// <summary>
        /// Encode la valeur au format MD5
        /// </summary>
        /// <param name="input">Valeur à encoder</param>
        /// <returns>La valeur encodée</returns>
        public static string HashMd5(string input)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm("MD5");
            var buff = CryptographicBuffer.ConvertStringToBinary(input, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            return CryptographicBuffer.EncodeToHexString(hashed);
        }

        /// <summary>
        /// Encode la valeur au format MD5
        /// </summary>
        /// <param name="input">Valeur à encoder</param>
        /// <returns>La valeur encodée</returns>
        public static string HashMd5(byte[] input)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm("MD5");
            var buff = CryptographicBuffer.CreateFromByteArray(input);
            var hashed = alg.HashData(buff);
            return CryptographicBuffer.EncodeToHexString(hashed);
        }
    }
}

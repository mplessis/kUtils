using System.Text;

namespace Kopigi.Portable.Convert
{
    /// <summary>
    /// Converti des byte en hexadécimal
    /// </summary>
    public class ByteConvert
    {
        /// <summary>
        /// Converti un byte en hexadécimal
        /// </summary>
        /// <param name="data">byte à convertir</param>
        /// <returns>hexadécimal au format string</returns>
        public static string ByteToHex(byte data)
        {
            return System.Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' ');
        }

        /// <summary>
        /// Converti un tableau de string en une chaine représentant les valeurs hexadécimales de ces bytes
        /// </summary>
        /// <param name="datas">Tableau de byte</param>
        /// <returns>string représentant les valeurs hexadécimales des bytes</returns>
        public static string BytesToHex(byte[] datas)
        {
            var builder = new StringBuilder(datas.Length * 3);
            foreach (byte data in datas)
                builder.Append(ByteToHex(data));
            return builder.ToString().ToUpper();
        }
    }
}

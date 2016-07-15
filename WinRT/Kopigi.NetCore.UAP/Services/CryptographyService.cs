using Kopigi.Portable.Interfaces;

namespace Kopigi.NetCore.UAP.Services
{
    public class CryptographyService : ICryptographyService
    {
        /// <summary>
        /// Permet de chiffrer une valeur en utilisant l'algorythme SHA256
        /// </summary>
        /// <param name="value">Valeu à chiffrer</param>
        /// <returns>La valeur chiffrée</returns>
        public string HashSha256(string value)
        {
            return Kopigi.NetCore.UAP.Cryptography.Sha256.HashValue(value);
        }
    }
}

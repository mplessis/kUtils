namespace Kopigi.Portable.Interfaces
{
    public interface ICryptographyService
    {
        /// <summary>
        /// Permet de chiffrer une valeur en utilisant l'algorythme SHA256
        /// </summary>
        /// <param name="value">Valeu à chiffrer</param>
        /// <returns>La valeur chiffrée</returns>
        string HashSha256(string value);
    }
}

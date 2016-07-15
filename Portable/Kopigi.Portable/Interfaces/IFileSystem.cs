using System.Collections.Generic;
using System.Threading.Tasks;
using Kopigi.Portable.Enums;

namespace Kopigi.Portable.Interfaces
{
    /// <summary>
    /// Interface de gestion de la persistance dans un fichier
    /// Permet de gérer cette persistance en WinRT ou bien une autre technologie
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Permet de supprimer un fichier du systéme
        /// </summary>
        /// <param name="filePath">Chemin du fichier à supprimer</param>
        /// <returns>Indique si l'opération s'est correctement déroulée</returns>
        Task<bool> DeleteFile(string filePath);

        /// <summary>
        /// Permet de savoir si le fichier est présent 
        /// </summary>
        /// <param name="nameFile">Nom du fichier à rechercher</param>
        /// <returns>Indique si le fichier est présent ou non</returns>
        Task<bool> IsFileExist(string nameFile);

        /// <summary>
        /// Permet de savoir si des fichiers contenant le pattern défini sont présents
        /// </summary>
        /// <param name="patternNameFile">Pattern à rechercher dans les noms de fichier</param>
        /// <returns>Indique si des fichiers sont présents</returns>
        Task<bool> IsFilesPresent(string patternNameFile);

        /// <summary>
        /// Permet de lister les fichiers contenant le patternNameFile dans leur nom
        /// </summary>
        /// <param name="patternNameFile">Pattern de nom de fichier à rechercher</param>
        /// <returns>Liste des noms de fichier retrouvés</returns>
        Task<List<string>> ListFiles(string patternNameFile);

        /// <summary>
        /// Permet de lire l'intégralité d'un fichier
        /// </summary>
        /// <param name="filePath">Fichier à lire</param>
        /// <returns>L'ensemble du fichier</returns>
        Task<string> ReadFile(string filePath);

        /// <summary>
        /// Permet de définir l'extension utilisée par l'application pour les fichiers de POCO
        /// </summary>
        /// <param name="extension">Nom de l'extension (sans le point)</param>
        void SetExtensionFile(string extension);

        /// <summary>
        /// Permet de définir le stockage à utiliser, par défaut local
        /// </summary>
        /// <param name="storage">Type de stockage à utiliser</param>
        void SetStorage(StorageEnum storage);

        /// <summary>
        /// Permet de persister des données dans le fichier spécifié
        /// </summary>
        /// <param name="data">Données à persister</param>
        /// <param name="filePath">Fichier à écrire</param>
        /// <returns>Indique si l'opération s'est correctement déroulée</returns>
        Task<bool> WriteInFile(string data, string filePath);
    }
}

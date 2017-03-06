using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using GalaSoft.MvvmLight.Ioc;
using Kopigi.Portable.Enums;
using Kopigi.Portable.Interfaces;

namespace Kopigi.NetCore.UAP.FileSystem
{
    /// <summary>
    /// Permet de gérer les fichiers pour la technologie WinRT
    /// </summary>
    public class FileSystemManager : IFileSystem
    {
        /// <summary>
        /// A redéfinir aprés initialisation de la classe
        /// </summary>
        private string _extensionFile;

        /// <summary>
        /// Indique si l'on utilise le stockage local, par défaut oui, mais peut être redéfini
        /// </summary>
        private bool _useLocalFolder;

        #region cstor

        [PreferredConstructor]
        public FileSystemManager()
        {
            _useLocalFolder = true;
        }

        public FileSystemManager(StorageEnum storage)
        {
            _useLocalFolder = storage == StorageEnum.Local;
        }
        #endregion

        /// <summary>
        /// Permet de supprimer un fichier du systéme
        /// </summary>
        /// <param name="filePath">Chemin du fichier à supprimer</param>
        /// <returns>Indique si l'opération s'est correctement déroulée</returns>
        public async Task<bool> DeleteFile(string filePath)
        {
            try
            {
                var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
                var fileName = !string.IsNullOrEmpty(_extensionFile) ? string.Format("{0}.{1}", filePath, _extensionFile) : filePath;
                var file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Permet de renvoyer le fichier demandé
        /// </summary>
        /// <param name="nameFile">Nom du fichier à rechercher</param>
        /// <returns>Le fichier demandé</returns>
        public async Task<object> GetFile(string nameFile)
        {
            var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
            if (await IsFileExist(nameFile))
            {
                return await folder.GetFileAsync(nameFile);
            }
            return null;
        }


        /// <summary>
        /// Permet de savoir si le fichier existe 
        /// </summary>
        /// <param name="nameFile">Nom du fichier à rechercher</param>
        /// <returns>Indique si le fichier est présent ou non</returns>
        public async Task<bool> IsFileExist(string nameFile)
        {
            try
            {
                var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
                await folder.GetFileAsync(nameFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Permet de savoir si des fichiers contenant le pattern défini sont présents
        /// </summary>
        /// <param name="patternNameFile">Pattern à rechercher dans les noms de fichier</param>
        /// <returns>Indique si des fichiers sont présents</returns>
        public async Task<bool> IsFilesPresent(string patternNameFile)
        {
            var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
            var files = await folder.GetFilesAsync();
            return files.Any(f => f.DisplayName.Contains(patternNameFile));
        }

        /// <summary>
        /// Permet de lister les fichiers contenant le patternNameFile dans leur nom
        /// </summary>
        /// <param name="patternNameFile">Pattern de nom de fichier à rechercher</param>
        /// <returns>Liste des noms de fichier retrouvés</returns>
        public async Task<List<string>> ListFiles(string patternNameFile)
        {
            var list = new List<string>();
            var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                if (file.DisplayName.Contains(patternNameFile))
                {
                    list.Add(file.DisplayName);
                }
            }
            return list;
        }

        /// <summary>
        /// Permet de lire l'intégralité d'un fichier
        /// </summary>
        /// <param name="filePath">Fichier à lire</param>
        /// <returns>L'ensemble du fichier</returns>
        public async Task<string> ReadFile(string filePath)
        {
            var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
            var fileName = !string.IsNullOrEmpty(_extensionFile) ? string.Format("{0}.{1}", filePath, _extensionFile) : filePath;
            var file = await folder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(file);
        }

        /// <summary>
        /// Permet de définir l'extension utilisée par l'application pour les fichiers de POCO
        /// </summary>
        /// <param name="extension">Nom de l'extension (sans le point)</param>
        public void SetExtensionFile(string extension)
        {
            _extensionFile = extension;
        }

        /// <summary>
        /// Permet de définir le stockage à utiliser, par défaut local
        /// </summary>
        /// <param name="storage">Type de stockage à utiliser</param>
        public void SetStorage(StorageEnum storage)
        {
            _useLocalFolder = storage == StorageEnum.Local;
        }

        /// <summary>
        /// Permet de persister des données (en string) dans le fichier spécifié
        /// </summary>
        /// <param name="data">Données à persister</param>
        /// <param name="filePath">Fichier à écrire</param>
        /// <returns>Indique si l'opération s'est correctement déroulée</returns>
        public async Task<object> WriteInFile(string data, string filePath)
        {
            try
            {
                var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
                var fileName = !string.IsNullOrEmpty(_extensionFile) ? string.Format("{0}.{1}", filePath, _extensionFile) : filePath;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, data);

                return file;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Permet de persister des données (en bytes) dans le fichier spécifié
        /// </summary>
        /// <param name="data">Données à persister</param>
        /// <param name="filePath">Fichier à écrire</param>
        /// <returns>Indique si l'opération s'est correctement déroulée</returns>
        public async Task<object> WriteInFile(byte[] data, string filePath)
        {
            try
            {
                var folder = _useLocalFolder ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
                var fileName = !string.IsNullOrEmpty(_extensionFile) ? string.Format("{0}.{1}", filePath, _extensionFile) : filePath;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, data);
                
                return file;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

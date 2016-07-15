using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Kopigi.Portable.Convert;
using Kopigi.Portable.Interfaces;

namespace Kopigi.Portable.Data.POCO
{
    /// <summary>
    /// Helper autour des KPoco
    /// </summary>
    public static class KPocoHelper
    {

        /// <summary>
        /// Renvoi le nom du type de fichier voulu
        /// </summary>
        /// <typeparam name="T">type de l'objet</typeparam>
        /// <returns>Nom du type de fichier</returns>
        public static string GetTypeNameFile<T>()
        {
            return typeof(T).Name;
        }

        /// <summary>
        /// Le fichier est construit suivant le schéma : TypeObjet_GUID.json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objet"></param>
        /// <returns></returns>
        public static string GetFileNameFromObject<T>(T objet) where T : KPoco
        {
            return string.Format("{0}_{1}", GetTypeNameFile<T>(), objet.IdGuid);
        }

        /// <summary>
        /// Permet de lister les objets du type spécifié
        /// </summary>
        /// <typeparam name="T">Type de fichier à retrouver</typeparam>
        /// <returns>Liste des objets retrouvés</returns>
        public async static Task<List<T>> ListFiles<T>() where T : KPoco
        {
            var list = new List<T>();
            var task = Task.Run(() => SimpleIoc.Default.GetInstance<IFileSystem>().ListFiles(GetTypeNameFile<T>()));
            var files = await task;
            foreach (var file in files)
            {
                var obj = await Read<T>(file);
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// Permet de lire un fichier et d'en extraire l'objet
        /// </summary>
        /// <typeparam name="T">Type de l'objet à obtenir</typeparam>
        /// <param name="pathFile">Fichier à lire</param>
        /// <returns>Objet retrouvé dans le fichier ou null</returns>
        public async static Task<T> Read<T>(string pathFile) where T : KPoco
        {
            var task = Task.Run(() => SimpleIoc.Default.GetInstance<IFileSystem>().ReadFile(pathFile));
            var value = await task;
            if (!string.IsNullOrEmpty(value))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
            return null;
        }

        /// <summary>
        /// Permet de lire un fichier et d'en extraire l'objet
        /// </summary>
        /// <typeparam name="T">Type de l'objet à obtenir</typeparam>
        /// <param name="idGuid">Identifiant Guid de l'objet à obtenir</param>
        /// <returns>Objet retrouvé dans le fichier ou null</returns>
        public async static Task<T> Read<T>(Guid idGuid) where T : KPoco
        {
            var pathFile = string.Format("{0}_{1}", GetTypeNameFile<T>(), idGuid);
            var task = Task.Run(() => SimpleIoc.Default.GetInstance<IFileSystem>().ReadFile(pathFile));
            var value = await task;
            if (!string.IsNullOrEmpty(value))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
            return null;
        }
    }
}

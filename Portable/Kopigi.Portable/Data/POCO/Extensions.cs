using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Kopigi.Portable.Convert;
using Kopigi.Portable.Interfaces;

namespace Kopigi.Portable.Data.POCO
{
    /// <summary>
    /// Extensions pour POCO
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Permet de supprimer un objet du systéme de fichier
        /// </summary>
        /// <typeparam name="T">Type de l'objet à supprimer</typeparam>
        /// <param name="objet">Objet à supprimer</param>
        public static async Task<bool> Delete<T>(this T objet) where T : KPoco
        {
            return await SimpleIoc.Default.GetInstance<IFileSystem>().DeleteFile(KPocoHelper.GetFileNameFromObject(objet));
        }

        /// <summary>
        /// Persiste la valeur au format json dans le fichier spécifié
        /// </summary>
        /// <typeparam name="T">Type de l'objet à persister</typeparam>
        /// <param name="objet">Objet à persister</param>
        public async static Task<bool> Persist<T>(this T objet) where T : KPoco
        {
            var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(objet);
            return (await SimpleIoc.Default.GetInstance<IFileSystem>().WriteInFile(dataJson, KPocoHelper.GetFileNameFromObject(objet))) != null;
        }
    }
}

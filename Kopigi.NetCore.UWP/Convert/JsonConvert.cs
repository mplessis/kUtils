using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Kopigi.Portable.Object;

namespace Kopigi.Portable.Convert
{
    /// <summary>
    /// Permet la sérialisation d'objet .NET vers le format JSON mais également la désérialisation de données JSON en objets .NET
    /// </summary>
    public static class JsonConvert
    {
        /// <summary>
        /// Converti un objet <see cref="DynamicObjet"/> au format JSON
        /// </summary>
        /// <returns></returns>
        public static string ConvertDynamicObjet(DynamicObjet objet)
        {
            var stringBuilder = new StringBuilder("{");
            stringBuilder.Append(String.Join(",", ((IDictionary<string, object>)objet.Dynamic).Select(kvp => String.Format("\"{0}\":{1}", kvp.Key, JsonConvert.Serialize(kvp.Value))).ToArray()));
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Permet de sérialiser un objet au format JSON </summary>
        /// <typeparam name="T">Type de l'objet à sérialiser</typeparam>
        /// <param name="element">Objet à sérialiser</param>
        /// <returns>La donnée au format JSON</returns>
        public static string Serialize<T>(T element)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, element);
                return Encoding.UTF8.GetString(stream.ToArray(), 0, stream.ToArray().Length);
            }
        }

        /// <summary>
        /// Permet de désérialiser une phrase JSON pour la convertir en objet .NET
        /// </summary>
        /// <typeparam name="T">Type voulu en sortie</typeparam>
        /// <param name="jsonData">Donnée JSON</param>
        /// <returns>L'objet voulu</returns>
        public static T Deserialize<T>(string jsonData)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}

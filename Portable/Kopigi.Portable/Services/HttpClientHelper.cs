using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Kopigi.Portable.Convert;
using HttpClient = System.Net.Http.HttpClient;
using Kopigi.Portable.Object;

namespace Kopigi.Portable
{
    public class HttpClientHelper
    {
        /// <summary>
        /// Renvoie un objet <see cref="HttpClient" /> avec les options de Header qu'il faut
        /// </summary>
        /// <returns>un objet <see cref="HttpClient"/></returns>
        public static HttpClient HttpClient(bool noCache = false)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (noCache)
            {
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            }
            return client;
        }

        /// <summary>
        /// Permet d'ajouter une valeur personnelle aux entêtes de la requête 
        /// </summary>
        /// <param name="name">Nom de la variable à ajouter</param>
        /// <param name="value">Valeur de cette variable</param>
        /// <param name="client">HttpClient en cours</param>
        public static void AddHeader(string name, string value, HttpClient client)
        {
            client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Permet de construire l'url d'une opération
        /// </summary>
        /// <param name="url">Url du service à consulter</param>
        /// <param name="operation">Nom de l'opération à ajouter à l'url du service</param>
        /// <param name="parameters">Paramètres éventuels à ajouter à l'url</param>
        /// <param name="uriKind">Type de l'uri à obtenir, par défaut il s'agit d'une uri de type absolut"/></param>
        /// <returns>Une url au format <see cref="Uri"/></returns>
        public static Uri BuildUri(string url, string operation, object[] parameters, UriKind uriKind = UriKind.Absolute)
        {
            return new Uri(BuildUrl(url, operation, parameters), uriKind);
        }

        /// <summary>
        /// Permet de construire l'url d'une opération
        /// </summary>
        /// <param name="url">Url du service à consulter</param>
        /// <param name="operation">Nom de l'opération à ajouter à l'url du service</param>
        /// <param name="parameters">Paramètres éventuels à ajouter à l'url</param>
        /// <returns>Une url au format <see cref="string"/></returns>
        public static string BuildUrl(string url, string operation, params object[] parameters)
        {
            var urlBuilded = string.Format("{0}{1}", url, operation);
            return parameters.Aggregate(urlBuilded, (current, parameter) => string.Format("{0}/{1}", current, parameter));
        }

        /// <summary>
        /// Prépare le contenu JSON pour l'envoi au service
        /// </summary>
        /// <param name="obj">Objet à sérialiser</param>
        /// <returns>Un <see cref="StringContent"/> à passer lors de l'envoi de la requête au service</returns>
        public static StringContent PrepareStringContentJson<T>(T obj)
        {
            return new StringContent(JsonConvert.Serialize(obj), Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Prépare le contenu JSON pour l'envoi au service
        /// </summary>
        /// <param name="parameters">Ensemble de paramétres avec leur valeurs</param>
        /// <returns>Un <see cref="FormUrlEncodedContent"/> à passer lors de l'envoi de la requête au service</returns>
        public static StringContent PrepareStringContentJson(List<KeyValuePair<string, object>> parameters)
        {
            var jsonObject = new DynamicObjet(parameters);
            return new StringContent(JsonConvert.ConvertDynamicObjet(jsonObject), Encoding.UTF8, "application/json");
        }
    }
}

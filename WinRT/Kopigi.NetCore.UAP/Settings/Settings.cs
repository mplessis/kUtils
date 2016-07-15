using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Kopigi.Portable.Interfaces;

namespace Kopigi.NetCore.UAP.Settings
{
    public class Settings : ISettings
    {
        private bool _useRoamingFolder;

        /// <summary>
        /// Permet d'initialiser les paramètres
        /// </summary>
        /// <param name="useRoamingSettings">Indique si l'on souhaite stocker ces paramétres dans le Roaming Folder, par défaut on utilise le Local Folder</param>
        public void InitializeSettings(bool useRoamingSettings = false)
        {
            _useRoamingFolder = useRoamingSettings;
        }

        /// <summary>
        /// Permet d'initialiser les paramètres
        /// </summary>
        /// <param name="settings">Un dictionnaire comportant des paramètres par défaut à ajouter</param>
        /// <param name="useRoamingSettings">Indique si l'on souhaite stocker ces paramétres dans le Roaming Folder, par défaut on utilise le Local Folder</param>
        public void InitializeSettings(Dictionary<string, object> settings, bool useRoamingSettings = false)
        {
            _useRoamingFolder = useRoamingSettings;
            foreach (var setting in settings)
            {
                Save(setting.Key, setting.Value);
            }
        }

        /// <summary>
        /// Enregistre un paramétre
        /// </summary>
        /// <param name="name">Nom du paramétre</param>
        /// <param name="value">Valeur du paramétre</param>
        public void Save(string name, object value)
        {
            var localSettings = _useRoamingFolder ? Windows.Storage.ApplicationData.Current.RoamingSettings : Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[name] = value;
        }

        /// <summary>
        /// Supprime un paramétre
        /// </summary>
        /// <param name="name">Nom du paramétre</param>
        public void Delete(string name)
        {
            var localSettings = _useRoamingFolder ? Windows.Storage.ApplicationData.Current.RoamingSettings : Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove(name);
        }

        /// <summary>
        /// Récupére la valeur d'un paramétre, en la convertissant dans le Type demandé
        /// </summary>
        /// <typeparam name="T">Type voulu en retour</typeparam>
        /// <param name="name">Nom du paramétre à rechercher</param>
        /// <returns>Valeur du paramétre convertie dans le Type demandé</returns>
        public T GetValue<T>(string name)
        {
            var localSettings = _useRoamingFolder ? Windows.Storage.ApplicationData.Current.RoamingSettings : Windows.Storage.ApplicationData.Current.LocalSettings;
            var value = localSettings.Values[name];
            if (value != null)
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
            if (typeof(T).Name.ToUpper() == "STRING")
            {
                return (T)Convert.ChangeType(string.Empty, typeof(T), CultureInfo.InvariantCulture);
            }
            return Activator.CreateInstance<T>();
        }
    }
}

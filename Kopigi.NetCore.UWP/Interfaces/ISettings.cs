using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Portable.Interfaces
{
    public interface ISettings
    {
        /// <summary>
        /// Permet d'initialiser les paramètres
        /// </summary>
        /// <param name="useRoamingSettings">Indique si l'on souhaite stocker ces paramétres dans le Roaming Folder, par défaut on utilise le Local Folder</param>
        void InitializeSettings(bool useRoamingSettings = false);

        /// <summary>
        /// Initialise des valeurs de paramétres
        /// </summary>
        /// <param name="useRoamingSettings">Indique si l'on souhaite stocker ces paramétres dans le Roaming Folder, par défaut on utilise le Local Folder</param>
        void InitializeSettings(Dictionary<string, object> settings, bool useRoamingSettings = false);

        /// <summary>
        /// Détermine si le paramètre est déja déclaré ou non
        /// </summary>
        /// <param name="name">Nom du paramèetre</param>
        /// <returns>Indique si il existe ou pas</returns>
        bool IsExist(string name);

        /// <summary>
        /// Enregistre un paramétre
        /// </summary>
        /// <param name="name">Nom du paramétre</param>
        /// <param name="value">Valeur du paramétre</param>
        void Save(string name, object value);

        /// <summary>
        /// Supprime un paramétre
        /// </summary>
        /// <param name="name">Nom du paramétre à supprimer</param>
        void Delete(string name);

        /// <summary>
        /// Renvoie la valeur d'un paramétre en le convertissant vers le type T
        /// </summary>
        /// <typeparam name="T">Type de la valeur de retour</typeparam>
        /// <param name="name">Nom du paramétre à chercher</param>
        /// <returns>Valeur du paramétre convertie vers T</returns>
        T GetValue<T>(string name);
    }
}

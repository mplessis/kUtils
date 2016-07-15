using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Portable.Interfaces
{
    public interface IResources
    {
        /// <summary>
        /// Permet de définir un fichier de resource
        /// </summary>
        /// <param name="file">Chemin du fichier</param>
        void SetResourcesFile(string file);

        /// <summary>
        /// Permet de récupérer La valeur d'une ressource
        /// </summary>
        /// <param name="ressource">Nom de la ressource</param>
        /// <returns>Valeur de la ressource</returns>
        string GetString(object ressource);
    }
}

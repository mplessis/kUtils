using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Kopigi.Portable.Interfaces;

namespace Kopigi.NetCore.UAP.Resources
{
    /// <summary>
    /// Permet de gérer les ressources
    /// </summary>
    public class Resources : IResources
    {
        private Windows.ApplicationModel.Resources.ResourceLoader _resourceLoader;

        public void SetResourcesFile(string file)
        {
            if (_resourceLoader == null)
            {
                _resourceLoader = new ResourceLoader(file);
            }    
        }

        /// <summary>
        /// Permet de récupérer La valeur d'une ressource
        /// </summary>
        /// <param name="ressource">Nom de la ressource</param>
        /// <returns>Valeur de la ressource</returns>
        public string GetString(object ressource)
        {
            var loader = _resourceLoader ?? (_resourceLoader = ResourceLoader.GetForCurrentView());
            return loader.GetString(ressource.ToString());
        }
    }
}

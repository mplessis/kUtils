using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Portable.Convert;

namespace Kopigi.Portable.Object
{
    public class DynamicObjet
    {
        #region fields

        private ExpandoObject _expandoObject = new ExpandoObject();

        #endregion

        #region properties

        public dynamic Dynamic
        {
            get { return _expandoObject; }
        }

        #endregion

        public DynamicObjet()
        { }

        /// <summary>
        /// Constructeur permettant d'ajouter directement un IENumerable de KeyValuePair
        /// </summary>
        /// <param name="kvp"></param>
        public DynamicObjet(IEnumerable<KeyValuePair<string, object>> kvp):this()
        {
            foreach (var pair in kvp)
            {
                Add(pair);
            }
        }

        /// <summary>
        /// Ajoute un couple propriété/valeur à la collection
        /// </summary>
        /// <param name="kvp">LE KeyValuePair à ajouter</param>
        public void Add(KeyValuePair<string, object> kvp)
        {
            Add(kvp.Key, kvp.Value);
        }

        /// <summary>
        /// Ajoute un couple propriété/valeur à la collection
        /// </summary>
        /// <param name="property">Nom de la propriété</param>
        /// <param name="value">Valeur de la propriété</param>
        public void Add(string property, object value)
        {
            ((IDictionary<string, object>)_expandoObject).Add(new KeyValuePair<string, object>(property, value));
        }

        /// <summary>
        /// Efface l'ensemble de la collection
        /// </summary>
        public void Clear()
        {
            ((IDictionary<string, object>)_expandoObject).Clear();
        }

        /// <summary>
        /// Supprime une propriété de la collection
        /// </summary>
        /// <param name="property">Nom de la propriété à supprimer</param>
        public void Remove(string property)
        {
            ((IDictionary<string, object>)_expandoObject).Remove(property);
        }
    }
}

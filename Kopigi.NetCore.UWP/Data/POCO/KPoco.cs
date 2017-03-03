using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Kopigi.Portable.Object;

namespace Kopigi.Portable.Data.POCO
{
    /// <summary>
    /// Définition de base pour tout POCO
    /// </summary>
    public class KPoco : NotifyPropertyChanged
    {
        /// <summary>
        /// Identifiant unique de la ligne
        /// </summary>
        public Guid IdGuid { get; set; }

        /// <summary>
        /// Permet de générer automatiquement un GUID pour l'élément
        /// </summary>
        public KPoco()
        {
            if (IdGuid == Guid.Empty)
            {
                IdGuid = Guid.NewGuid();
            }
        }
    }
}

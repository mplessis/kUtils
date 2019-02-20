using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kopigi.Utils.Extensions
{
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Permet d'ajouter une liste de T directement dans une ObservableCollection
        /// </summary>
        /// <param name="elementsToAdd">Liste d'objets à ajouter</param>
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> elementsToAdd)
        {
            foreach (var element in elementsToAdd)
            {
                observableCollection.Add(element);
            }
        }
    }
}

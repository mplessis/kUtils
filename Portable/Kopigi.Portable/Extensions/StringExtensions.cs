using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Portable.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Permet d'exporter une liste de T, vers une string dont chaque valeur est séparée par un char défini
        /// </summary>
        /// <param name="list">Liste d'objets à séparer</param>
        /// <param name="separate">Caractére de séparation</param>
        /// <returns></returns>
        public static string ToSeparateByChar<T>(this IEnumerable<T> list, char separate)
        {
            if (list.Any())
            {
                var itemsSeparateBuilder = new StringBuilder();
                foreach (var item in list)
                {
                    itemsSeparateBuilder.Append($"{item.ToString()}{separate}");
                }
                return itemsSeparateBuilder.Remove(itemsSeparateBuilder.Length - 1, 1).ToString();
            }
            return string.Empty;
        }
    }
}

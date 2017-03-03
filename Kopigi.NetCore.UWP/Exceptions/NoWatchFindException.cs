using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Portable.Exceptions
{
    public class NoWatchFindException : Exception
    {
        /// <summary>
        /// Constructeur de l'exception
        /// </summary>
        /// <param name="label">Label de surveillance demandé</param>
        public NoWatchFindException(string label)
            : base($"No watch found for label {label}", null)
        { }
    }
}

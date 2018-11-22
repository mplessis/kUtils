using System;

namespace Kopigi.Utils.Class
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

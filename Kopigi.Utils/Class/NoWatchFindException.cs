using System;
using System.Runtime.Serialization;

namespace Kopigi.Utils.Class
{
    [Serializable]
    public class NoWatchFindException : Exception
    {
        /// <summary>
        /// Constructeur de l'exception
        /// </summary>
        /// <param name="label">Label de surveillance demandé</param>
        public NoWatchFindException(string label)
            : base($"No watch found for label {label}", null)
        { }

        protected NoWatchFindException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}

using System;

namespace Kopigi.Portable.Class {

    public static class StringFormatUseCase {

        /// <summary>
        /// Met en majuscule ou minuscule la sortie du formatage
        /// </summary>
        /// <param name="isUpperCase"><c>true</c> indique une sortie en majuscule, <c>false</c> une sortie TOUT en minuscule</param>
        /// <param name="format">format désiré</param>
        /// <param name="args">valeurs à formater</param>
        /// <returns>valeurs formatées</returns>
        public static string Format(bool isUpperCase, string format, params object[] args)
        {
            return isUpperCase ? string.Format(format, args).ToUpper() : string.Format(format, args).ToLower();
        }
    }
}

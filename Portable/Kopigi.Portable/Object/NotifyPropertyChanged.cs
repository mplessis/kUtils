using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Kopigi.Portable.Object
{
    /// <summary>
    /// Permet d'implémenter la notification de changement de valeur des properiétés. Implémentation de l'interface <see cref="INotifyPropertyChanged"/>
    /// </summary>
    /// <example>
    ///
    /// Définition de votre propriété et dans le set
    /// ...
    /// RaisePropertyChanged();
    /// </example>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null) OnPropertyChanged(propertyName);
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            if (propertyName != null) OnPropertyChanged(propertyName);
        }

        #region protected

        /// <summary>
        /// Recherche du nom de la propriété depuis une Expression Inspiré de ObservableObject de MVVMLight écrit par Laurent Bugnion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
               throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }
            return propertyInfo.Name;
        }
        #endregion
    }
}

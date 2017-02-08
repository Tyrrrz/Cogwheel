using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Abstract class implementing <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Triggered when a settings property has been changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Whether to silence <see cref="PropertyChanged"/> event
        /// </summary>
        protected bool SilenceEvents { get; set; }

        private void RaisePropertyChangedInternal(string propertyName)
        {
            if (SilenceEvents) return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the property changed event for given property
        /// </summary>
        protected virtual void RaisePropertyChanged(
#if Net45
            [CallerMemberName]
#endif
            string propertyName = null)
        {
            RaisePropertyChangedInternal(propertyName);
        }

        /// <summary>
        /// Raises the property changed event for given property
        /// </summary>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = ((propertyExpression?.Body as MemberExpression)?.Member as PropertyInfo)?.Name;
            if (string.IsNullOrWhiteSpace(propertyName)) return;
            RaisePropertyChangedInternal(propertyName);
        }

        /// <summary>
        /// Sets a property to its new value and raises events as necessary
        /// </summary>
        protected virtual bool Set<T>(ref T field, T value,
#if Net45
            [CallerMemberName]
#endif
            string propertyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            RaisePropertyChangedInternal(propertyName);
            return true;
        }
    }
}

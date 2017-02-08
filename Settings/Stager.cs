using System;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Stages changes in a settings manager object, exposing them only after they are saved
    /// </summary>
    /// <typeparam name="T">Type of this object</typeparam>
    public class Stager<T> where T : SettingsManager
    {
        /// <summary>
        /// Current settings manager, all properties are up to date with the persistently stored version. Use only as read-only.
        /// </summary>
        public T Current { get; }

        /// <summary>
        /// Staging settings manager, properties may be unsynchronized with the current settings manager, until they are saved to persistent storage.
        /// </summary>
        public T Staging { get; }

        /// <summary>
        /// Create a stager for a settings manager with the default configuration
        /// </summary>
        public Stager()
        {
            Current = (T) Activator.CreateInstance(typeof (T));
            Staging = (T) Activator.CreateInstance(typeof (T));
        }

        /// <summary>
        /// Creates a stager for a settings manager, using a factory delegate
        /// </summary>
        public Stager(Func<T> settingsFactory)
        {
            Current = settingsFactory();
            Staging = settingsFactory();
        }

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        public virtual void Save()
        {
            Staging.Save();
            Current.CopyFrom(Staging);
        }

        /// <summary>
        /// Tries to save settings to file. If the operation fails - no exception is thrown.
        /// </summary>
        public virtual bool TrySave()
        {
            try
            {
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads settings from file (if it exists)
        /// </summary>
        public virtual void Load()
        {
            Staging.Load();
            Current.CopyFrom(Staging);
        }

        /// <summary>
        /// Tries to load settings from file if it exists. If the operation fails - no exception is thrown.
        /// </summary>
        public virtual bool TryLoad()
        {
            try
            {
                Load();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Resets staging settings back to current
        /// </summary>
        public virtual void RevertStaging()
        {
            Staging.CopyFrom(Current);
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Derive from this class to create a custom settings manager that can de-/serialize its public properties from/to persistent storage
    /// </summary>
    public abstract class SettingsManager : ObservableObject
    {
        [IgnoreDataMember] private bool _isSaved = true;

        /// <summary>
        /// Storage directory for the settings file
        /// </summary>
        [IgnoreDataMember] public string StorageDirectory { get; }

        /// <summary>
        /// Settings file path
        /// </summary>
        [IgnoreDataMember] public string SettingsFilePath { get; }

        /// <summary>
        /// Whether the settings have been saved since the last time they were changed
        /// </summary>
        [IgnoreDataMember]
        public bool IsSaved
        {
            get { return _isSaved; }
            protected set
            {
                if (_isSaved == value)
                    return;
                _isSaved = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Creates a settings manager object with custom configuration
        /// </summary>
        protected SettingsManager(Configuration configuration)
        {
            // Configure settings manager
            string storageSpacePath;
            switch (configuration.FileStorageSpace)
            {
                default:
                    storageSpacePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    break;
                case Configuration.StorageSpace.LocalAppData:
                    storageSpacePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
                case Configuration.StorageSpace.MyDocuments:
                    storageSpacePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
            }
            StorageDirectory = Path.Combine(storageSpacePath, configuration.SubdirectoryPath);
            SettingsFilePath = Path.Combine(StorageDirectory, configuration.FileName);
        }

        /// <summary>
        /// Creates a settings manager with default configuration
        /// </summary>
        protected SettingsManager() : this(new Configuration(Assembly.GetCallingAssembly().GetName().Name))
        {
        }

        /// <inheritdoc />
        protected override bool Set<T>(ref T field, T value,
#if Net45
            [CallerMemberName]
#endif
        string propertyName = null)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            bool changed = base.Set(ref field, value, propertyName);
            if (changed)
                IsSaved = false;
            return changed;
        }

        /// <summary>
        /// Copies values of the public and writeable properties from the given settings manager to the current
        /// </summary>
        public virtual void CopyFrom(SettingsManager referenceSettingsManager)
        {
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(referenceSettingsManager), this);
            IsSaved = referenceSettingsManager.IsSaved;
        }

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        public virtual void Save()
        {
            // Create the directory
            try
            {
                Directory.CreateDirectory(StorageDirectory);
            }
            catch
            {
                // Ignored
            }

            // Write file
            File.WriteAllText(SettingsFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
            IsSaved = true;
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
        /// Loads settings from file if it exists
        /// </summary>
        public virtual void Load()
        {
            if (!File.Exists(SettingsFilePath)) return;
            JsonConvert.PopulateObject(File.ReadAllText(SettingsFilePath), this);
            IsSaved = true;
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
        /// Resets settings back to default values
        /// </summary>
        public virtual void Reset()
        {
            var referenceSettings = (SettingsManager) Activator.CreateInstance(GetType());
            CopyFrom(referenceSettings);
            IsSaved = false;
        }

        /// <summary>
        /// Deletes settings file
        /// </summary>
        public virtual void Delete(bool deleteStorageDirectory = false)
        {
            if (deleteStorageDirectory && Directory.Exists(StorageDirectory))
                Directory.Delete(StorageDirectory, true);
            else if (File.Exists(SettingsFilePath))
                File.Delete(SettingsFilePath);
        }
    }
}
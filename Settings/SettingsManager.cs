using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Derive from this class to create a custom settings manager that can de-/serialize its public properties from/to file
    /// </summary>
    public abstract class SettingsManager : ObservableObject, ICloneable
    {
        [IgnoreDataMember]
        private bool _isSaved = true;

        /// <summary>
        /// Configuration object
        /// </summary>
        [IgnoreDataMember]
        public Configuration Configuration { get; set; }

        /// <summary>
        /// Whether the settings have been saved since the last time they were changed
        /// </summary>
        [IgnoreDataMember]
        public bool IsSaved
        {
            get { return _isSaved; }
            protected set
            {
                // ReSharper disable once ExplicitCallerInfoArgument
                base.Set(ref _isSaved, value, nameof(IsSaved));
            }
        }

        /// <summary>
        /// Creates a settings manager object with custom configuration
        /// </summary>
        protected SettingsManager(Configuration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Creates a settings manager with default configuration
        /// </summary>
        protected SettingsManager()
        {
            Configuration = new Configuration
            {
                SubDirectoryPath = Assembly.GetCallingAssembly().GetName().Name,
                FileName = GetType().Name + ".dat"
            };
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
            if (changed) IsSaved = false;
            return changed;
        }

        /// <summary>
        /// Copies values of accessable properties from the given settings manager into the current
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
            Directory.CreateDirectory(Configuration.FullDirectoryPath);

            // Write file
            File.WriteAllText(Configuration.FullFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
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
            if (!File.Exists(Configuration.FullFilePath)) return;
            JsonConvert.PopulateObject(File.ReadAllText(Configuration.FullFilePath), this);
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
        /// Deletes the settings file and, optionally, the containing directory
        /// </summary>
        public virtual void Delete(bool deleteStorageDirectory = false)
        {
            if (deleteStorageDirectory && Directory.Exists(Configuration.FullDirectoryPath))
                Directory.Delete(Configuration.FullDirectoryPath, true);
            else if (File.Exists(Configuration.FullFilePath))
                File.Delete(Configuration.FullFilePath);
        }

        /// <summary>
        /// Clones this <see cref="SettingsManager"/> along with current values of its properties
        /// </summary>
        public object Clone()
        {
            var clone = (SettingsManager) Activator.CreateInstance(GetType());
            clone.CopyFrom(this);
            return clone;
        }
    }
}
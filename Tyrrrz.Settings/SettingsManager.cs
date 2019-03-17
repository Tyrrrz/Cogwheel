using System;
using System.IO;
using System.Reflection;
using Tyrrrz.Settings.Serialization;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Derive from this class to create a custom settings manager that can de-/serialize its public properties from/to file
    /// </summary>
    public abstract partial class SettingsManager
    {
        private bool _isSaved = true;

        /// <summary>
        /// Configuration for this <see cref="SettingsManager"/> instance
        /// </summary>
        [Ignore]
        public Configuration Configuration { get; set; }

        /// <summary>
        /// Full path of the storage directory
        /// </summary>
        [Ignore]
        public string FullDirectoryPath
        {
            get
            {
                var result = Configuration.StorageSpace.GetDirectoryPath();
                if (!string.IsNullOrEmpty(Configuration.SubDirectoryPath))
                    result = Path.Combine(result, Configuration.SubDirectoryPath);
                return result;
            }
        }

        /// <summary>
        /// Full path of the settings file
        /// </summary>
        [Ignore]
        public string FullFilePath => Path.Combine(FullDirectoryPath, Configuration.FileName);

        /// <summary>
        /// Whether the settings have been saved since the last time they were changed
        /// </summary>
        [Ignore]
        public bool IsSaved
        {
            get => _isSaved;
            protected set => Set(ref _isSaved, value);
        }

        /// <summary>
        /// Creates a settings manager
        /// </summary>
        protected SettingsManager()
        {
            // Wire to its own INotifyPropertyChanged to handle IsSaved
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(IsSaved))
                    IsSaved = false;
            };

            // Set default configuration
            Configuration = new Configuration
            {
                SubDirectoryPath = Assembly.GetCallingAssembly().GetName().Name,
                FileName = GetType().Name + ".dat"
            };
        }

        /// <summary>
        /// Copies values of accessable properties from the given settings manager into the current
        /// </summary>
        public virtual void CopyFrom(SettingsManager referenceSettingsManager)
        {
            if (referenceSettingsManager == null)
                throw new ArgumentNullException(nameof(referenceSettingsManager));

            var serialized = Serializer.Serialize(referenceSettingsManager);
            Serializer.Populate(serialized, this);

            IsSaved = referenceSettingsManager.IsSaved;
        }

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        public virtual void Save()
        {
            try
            {
                // Create the directory
                Directory.CreateDirectory(FullDirectoryPath);

                // Write file
                var serialized = Serializer.Serialize(this);
                File.WriteAllText(FullFilePath, serialized);

                IsSaved = true;
            }
            catch
            {
                if (Configuration.ThrowIfCannotSave)
                    throw;
            }
        }

        /// <summary>
        /// Loads settings from file
        /// </summary>
        public virtual void Load()
        {
            try
            {
                if (!File.Exists(FullFilePath)) return;

                var serialized = File.ReadAllText(FullFilePath);

                // HACK: reverse compatibility
                // older versions of this library incorrectly saved data
                serialized = serialized.Replace("\0", "");

                Serializer.Populate(serialized, this);

                IsSaved = true;
            }
            catch
            {
                if (Configuration.ThrowIfCannotLoad)
                    throw;
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
        public virtual void Delete(bool deleteParentDirectory = false)
        {
            if (deleteParentDirectory)
            {
                try
                {
                    Directory.Delete(FullDirectoryPath, true);
                }
                catch (DirectoryNotFoundException)
                {
                }
            }
            else
            {
                try
                {
                    File.Delete(FullFilePath);
                }
                catch (FileNotFoundException)
                {
                }
            }
        }
    }
}
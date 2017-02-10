using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Tyrrrz.Settings.Services;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Derive from this class to create a custom settings manager that can de-/serialize its public properties from/to file
    /// </summary>
    public abstract class SettingsManager : ObservableObject, ICloneable
    {
        private bool _isSaved = true;

        /// <summary>
        /// Configuration object
        /// </summary>
        [IgnoreProperty]
        public Configuration Configuration { get; }

        /// <summary>
        /// Shortcut for serializer
        /// </summary>
        [IgnoreProperty]
        private ISerializationService SerializationService => Configuration.SerializationService;

        /// <summary>
        /// Shortcut for file system handler
        /// </summary>
        [IgnoreProperty]
        private IFileSystemService FileSystemService => Configuration.FileSystemService;

        /// <summary>
        /// Whether the settings have been saved since the last time they were changed
        /// </summary>
        [IgnoreProperty]
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
            var serialized = SerializationService.Serialize(referenceSettingsManager);
            SerializationService.Populate(serialized, this);
            IsSaved = referenceSettingsManager.IsSaved;
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

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        public virtual void Save()
        {
            // Create the directory
            FileSystemService.CreateDirectory(Configuration.FullDirectoryPath);

            // Write file
            var serialized = SerializationService.Serialize(this);
            FileSystemService.FileWriteAllBytes(Configuration.FullFilePath, serialized);
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
            if (!FileSystemService.FileExists(Configuration.FullFilePath)) return;
            var serialized = FileSystemService.FileReadAllBytes(Configuration.FullFilePath);
            SerializationService.Populate(serialized, this);
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
        public virtual void Delete(bool deleteParentDirectory = false)
        {
            if (deleteParentDirectory)
            {
                FileSystemService.DeleteDirectory(Configuration.FullDirectoryPath, true);
            }
            else
            {
                FileSystemService.DeleteFile(Configuration.FullFilePath);
            }
        }
    }
}
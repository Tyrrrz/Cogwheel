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
        private readonly ISerializationService _serializationService;
        private readonly IFileSystemService _fileSystemService;

        private bool _isSaved = true;

        /// <summary>
        /// Configuration object
        /// </summary>
        [IgnoreProperty]
        public Configuration Configuration { get; set; }

        /// <summary>
        /// Full path of the storage directory
        /// </summary>
        [IgnoreProperty]
        public string FullDirectoryPath
        {
            get
            {
                string result = _fileSystemService.GetDirectoryLocation(Configuration.StorageSpace);
                if (!string.IsNullOrEmpty(Configuration.SubDirectoryPath))
                    result = _fileSystemService.CombinePath(result, Configuration.SubDirectoryPath);
                return result;
            }
        }

        /// <summary>
        /// Full path of the settings file
        /// </summary>
        [IgnoreProperty]
        public string FullFilePath => _fileSystemService.CombinePath(FullDirectoryPath, Configuration.FileName);

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
        /// Creates a settings manager with custom services
        /// </summary>
        protected SettingsManager(ISerializationService serializationService, IFileSystemService fileSystemService)
        {
            if (serializationService == null)
                throw new ArgumentNullException(nameof(serializationService));
            if (fileSystemService == null)
                throw new ArgumentNullException(nameof(fileSystemService));

            _serializationService = serializationService;
            _fileSystemService = fileSystemService;

            Configuration = new Configuration
            {
                SubDirectoryPath = Assembly.GetCallingAssembly().GetName().Name,
                FileName = GetType().Name + ".dat"
            };
        }

        /// <summary>
        /// Creates a settings manager with default services
        /// </summary>
        protected SettingsManager()
            : this(JsonNetSerializationService.Instance, DefaultFileSystemService.Instance)
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
            if (changed) IsSaved = false;
            return changed;
        }

        /// <summary>
        /// Copies values of accessable properties from the given settings manager into the current
        /// </summary>
        public virtual void CopyFrom(SettingsManager referenceSettingsManager)
        {
            if (referenceSettingsManager == null)
                throw new ArgumentNullException(nameof(referenceSettingsManager));

            var serialized = _serializationService.Serialize(referenceSettingsManager);
            _serializationService.Populate(serialized, this);
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
            _fileSystemService.CreateDirectory(FullDirectoryPath);

            // Write file
            var serialized = _serializationService.Serialize(this);
            _fileSystemService.FileWriteAllBytes(FullFilePath, serialized);
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
            if (!_fileSystemService.FileExists(FullFilePath)) return;
            var serialized = _fileSystemService.FileReadAllBytes(FullFilePath);
            _serializationService.Populate(serialized, this);
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
                _fileSystemService.DeleteDirectory(FullDirectoryPath, true);
            }
            else
            {
                _fileSystemService.DeleteFile(FullFilePath);
            }
        }
    }
}
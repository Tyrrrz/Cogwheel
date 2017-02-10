using Tyrrrz.Settings.Services;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Configuration object for the SettingsManager class
    /// </summary>
    public sealed class Configuration
    {
        private ISerializationService _serializationService = JsonNetSerializationService.Instance;
        private IFileSystemService _fileSystemService = DefaultFileSystemService.Instance;
        private StorageSpace _storageSpace = StorageSpace.SyncedUserDomain;
        private string _subDirectoryPath = string.Empty;
        private string _fileName = "Settings.dat";
        private string _fullDirectoryPath;
        private string _fullFilePath;

        /// <summary>
        /// Serialization abstraction
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public ISerializationService SerializationService
        {
            get { return _serializationService; }
            set { _serializationService = value; }
        }

        /// <summary>
        /// File system abstraction
        /// </summary>
        public IFileSystemService FileSystemService
        {
            get { return _fileSystemService; }
            set
            {
                _fileSystemService = value;
                UpdateFullDirectoryPath();
                UpdateFullFilePath();
            }
        }

        /// <summary>
        /// Type of storage, where the settings file will be stored
        /// </summary>
        public StorageSpace StorageSpace
        {
            get { return _storageSpace; }
            set
            {
                _storageSpace = value;
                UpdateFullDirectoryPath();
            }
        }

        /// <summary>
        /// Subdirectory path for where the settings file is stored, relative to the selected <see cref="StorageSpace"/>
        /// </summary>
        public string SubDirectoryPath
        {
            get { return _subDirectoryPath; }
            set
            {
                _subDirectoryPath = value;
                UpdateFullDirectoryPath();
            }
        }

        /// <summary>
        /// Name of the settings file
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                UpdateFullFilePath();
            }
        }

        /// <summary>
        /// Full path to the directory, where the settings are stored
        /// </summary>
        public string FullDirectoryPath
        {
            get { return _fullDirectoryPath; }
            private set
            {
                _fullDirectoryPath = value;
                UpdateFullFilePath();
            }
        }

        /// <summary>
        /// Full path to the file, where the settings are stored
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public string FullFilePath
        {
            get { return _fullFilePath; }
            private set { _fullFilePath = value; }
        }

        /// <inheritdoc />
        internal Configuration()
        {
            // Propagate defaults
            UpdateFullDirectoryPath();
            UpdateFullFilePath();
        }

        private void UpdateFullDirectoryPath()
        {
            string root = FileSystemService.GetDirectoryLocation(StorageSpace);
            FullDirectoryPath = FileSystemService.CombinePath(root, SubDirectoryPath);
        }

        private void UpdateFullFilePath()
        {
            FullFilePath = FileSystemService.CombinePath(FullDirectoryPath, FileName);
        }
    }
}
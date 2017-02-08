using System;
using System.IO;
using System.Reflection;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Configuration object for the SettingsManager class
    /// </summary>
    public sealed class Configuration
    {
        private StorageSpace _fileStorageSpace = StorageSpace.RoamingAppData;
        private string _subDirectoryPath = string.Empty;
        private string _fileName = "Settings.dat";
        private string _fullDirectoryPath;
        private string _fullFilePath;

        /// <summary>
        /// Type of storage, where the settings file will be stored
        /// </summary>
        public StorageSpace FileStorageSpace
        {
            get { return _fileStorageSpace; }
            set
            {
                _fileStorageSpace = value;
                UpdateFullDirectoryPath();
            }
        }

        /// <summary>
        /// Subdirectory path for where the settings file is stored, relative to the selected <see cref="FileStorageSpace"/>
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
            string root;
            switch (FileStorageSpace)
            {
                case StorageSpace.RoamingAppData:
                    root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    break;
                case StorageSpace.LocalAppData:
                    root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
                case StorageSpace.ProgramData:
                    root = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                    break;
                case StorageSpace.MyDocuments:
                    root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                case StorageSpace.Instance:
                    root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                           Environment.CurrentDirectory;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            FullDirectoryPath = Path.Combine(root, SubDirectoryPath);
        }

        private void UpdateFullFilePath()
        {
            FullFilePath = Path.Combine(FullDirectoryPath, FileName);
        }
    }
}
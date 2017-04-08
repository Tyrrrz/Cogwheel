namespace Tyrrrz.Settings
{
    /// <summary>
    /// Configuration object for the SettingsManager class
    /// </summary>
    public sealed class Configuration
    {
        /// <summary>
        /// Type of storage, where the settings file will be stored
        /// </summary>
        public StorageSpace StorageSpace { get; set; }

        /// <summary>
        /// Subdirectory path for where the settings file is stored, relative to the selected <see cref="StorageSpace"/>
        /// </summary>
        public string SubDirectoryPath { get; set; }

        /// <summary>
        /// Name of the settings file
        /// </summary>
        public string FileName { get; set; }
    }
}
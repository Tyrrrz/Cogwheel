namespace Tyrrrz.Settings
{
    /// <summary>
    /// Configuration for <see cref="SettingsManager"/>
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Type of abstract storage where the settings file will be stored
        /// </summary>
        public StorageSpace StorageSpace { get; set; } = StorageSpace.SyncedUserDomain;

        /// <summary>
        /// Subdirectory path for where the settings file is stored, relative to the selected <see cref="StorageSpace"/>
        /// </summary>
        public string SubDirectoryPath { get; set; }

        /// <summary>
        /// Name of the settings file
        /// </summary>
        public string FileName { get; set; } = "Settings.dat";

        /// <summary>
        /// Whether to throw an exception when the settings file cannot be saved
        /// </summary>
        public bool ThrowIfCannotSave { get; set; } = true;

        /// <summary>
        /// Whether to throw an exception when the settings file cannot be loaded
        /// </summary>
        public bool ThrowIfCannotLoad { get; set; } = false;
    }
}
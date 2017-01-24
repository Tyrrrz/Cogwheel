namespace Tyrrrz.Settings
{
    /// <summary>
    /// Determines where the settings files will be stored
    /// </summary>
    public enum StorageSpace
    {
        /// <summary>
        /// The settings file is stored in %appdata% folder
        /// </summary>
        RoamingAppData,

        /// <summary>
        /// The settings file is stored in %localappdata% folder
        /// </summary>
        LocalAppData,

        /// <summary>
        /// The settings file is stored in My Documents folder
        /// </summary>
        MyDocuments
    }
}
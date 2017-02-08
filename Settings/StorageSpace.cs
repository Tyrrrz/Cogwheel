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
        /// The settings file is stored in %programdata% folder
        /// </summary>
        ProgramData,

        /// <summary>
        /// The settings file is stored in My Documents folder
        /// </summary>
        MyDocuments,

        /// <summary>
        /// The settings file is stored in the same folder as this assembly
        /// </summary>
        Instance
    }
}
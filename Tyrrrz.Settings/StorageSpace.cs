namespace Tyrrrz.Settings
{
    /// <summary>
    /// Determines where the settings files will be stored
    /// </summary>
    public enum StorageSpace
    {
        /// <summary>
        /// The settings file is stored in synchronized user domain.
        /// Roaming Application Data on Windows.
        /// </summary>
        SyncedUserDomain,

        /// <summary>
        /// The settings file is stored in unsynchronized user domain.
        /// Local Application Data on Windows.
        /// </summary>
        UserDomain,

        /// <summary>
        /// The settings file is stored in machine domain.
        /// Program Data on Windows.
        /// </summary>
        MachineDomain,

        /// <summary>
        /// The settings file is stored in the current directory.
        /// </summary>
        Instance
    }
}
using System;

namespace Tyrrrz.Settings
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Get actual directory path for given storage space
        /// </summary>
        public static string GetDirectoryPath(this StorageSpace storageSpace)
        {
            switch (storageSpace)
            {
                case StorageSpace.SyncedUserDomain:
                    return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                case StorageSpace.UserDomain:
                    return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                case StorageSpace.MachineDomain:
                    return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                case StorageSpace.Instance:
                    return AppDomain.CurrentDomain.BaseDirectory;
                default:
                    throw new ArgumentOutOfRangeException(nameof(storageSpace), storageSpace, null);
            }
        }
    }
}
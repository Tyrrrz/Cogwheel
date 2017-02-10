using System;
using System.IO;

namespace Tyrrrz.Settings.Services
{
    /// <summary>
    /// Uses system's file system as persistent storage
    /// </summary>
    public class DefaultFileSystemService : IFileSystemService
    {
        /// <summary>
        /// Default instance
        /// </summary>
        public static DefaultFileSystemService Instance { get; } = new DefaultFileSystemService();

        /// <inheritdoc />
        public void CreateDirectory(string dirPath)
        {
            Directory.CreateDirectory(dirPath);
        }

        /// <inheritdoc />
        public string GetDirectoryLocation(StorageSpace storageSpace)
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
                    return Environment.CurrentDirectory;
                default:
                    throw new ArgumentOutOfRangeException(nameof(storageSpace), storageSpace, null);
            }
        }

        /// <inheritdoc />
        public bool DirectoryExists(string dirPath)
        {
            return Directory.Exists(dirPath);
        }

        /// <inheritdoc />
        public void DeleteDirectory(string dirPath, bool recursive)
        {
            try
            {
                Directory.Delete(dirPath, recursive);
            }
            catch (DirectoryNotFoundException)
            {
            }
        }

        /// <inheritdoc />
        public byte[] FileReadAllBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <inheritdoc />
        public void FileWriteAllBytes(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }

        /// <inheritdoc />
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <inheritdoc />
        public void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (FileNotFoundException)
            {
            }
        }

        /// <inheritdoc />
        public string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }
    }
}
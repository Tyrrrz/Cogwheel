using System;
using System.IO;

namespace Tyrrrz.Settings.Services
{
    /// <summary>
    /// Uses local file system as persistent storage
    /// </summary>
    public class LocalFileSystemService : IFileSystemService
    {
        /// <inheritdoc />
        public virtual void CreateDirectory(string dirPath)
        {
            Directory.CreateDirectory(dirPath);
        }

        /// <inheritdoc />
        public virtual string GetDirectoryLocation(StorageSpace storageSpace)
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
        public virtual bool DirectoryExists(string dirPath)
        {
            return Directory.Exists(dirPath);
        }

        /// <inheritdoc />
        public virtual void DeleteDirectory(string dirPath, bool recursive)
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
        public virtual byte[] FileReadAllBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <inheritdoc />
        public virtual void FileWriteAllBytes(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }

        /// <inheritdoc />
        public virtual bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <inheritdoc />
        public virtual void DeleteFile(string filePath)
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
        public virtual string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }
    }
}
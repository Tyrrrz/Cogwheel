namespace Tyrrrz.Settings.Services
{
    /// <summary>
    /// Abstraction over the file system used for storing and retrieving settings
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// Creates directories along the given path
        /// </summary>
        void CreateDirectory(string dirPath);

        /// <summary>
        /// Gets directory location for the given storage space
        /// </summary>
        string GetDirectoryLocation(StorageSpace storageSpace);

        /// <summary>
        /// Checks if the given directory exists
        /// </summary>
        bool DirectoryExists(string dirPath);

        /// <summary>
        /// Deletes the given directory if it exists
        /// </summary>
        void DeleteDirectory(string dirPath, bool recursive);

        /// <summary>
        /// Reads given file and return its content as bytes
        /// </summary>
        byte[] FileReadAllBytes(string filePath);

        /// <summary>
        /// Writes given bytes as content to a file
        /// </summary>
        void FileWriteAllBytes(string filePath, byte[] data);

        /// <summary>
        /// Checks if the given file exists
        /// </summary>
        bool FileExists(string filePath);

        /// <summary>
        /// Deletes the given file if it exists
        /// </summary>
        void DeleteFile(string filePath);

        /// <summary>
        /// Combines multiple paths into one
        /// </summary>
        string CombinePath(params string[] paths);
    }
}
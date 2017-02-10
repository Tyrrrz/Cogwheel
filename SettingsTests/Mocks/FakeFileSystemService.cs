using System.IO.Abstractions;
using Tyrrrz.Settings;
using Tyrrrz.Settings.Services;

namespace Settings.Tests.Mocks
{
    public class FakeFileSystemService : IFileSystemService
    {
        public static FakeFileSystemService Instance { get; } = new FakeFileSystemService();

        private readonly IFileSystem _fileSystem = new FileSystem();

        public void CreateDirectory(string dirPath)
        {
            _fileSystem.Directory.CreateDirectory(dirPath);
        }

        public string GetDirectoryLocation(StorageSpace storageSpace)
        {
            return storageSpace.ToString();
        }

        public bool DirectoryExists(string dirPath)
        {
            return _fileSystem.Directory.Exists(dirPath);
        }

        public void DeleteDirectory(string dirPath, bool recursive)
        {
            _fileSystem.Directory.Delete(dirPath, recursive);
        }

        public byte[] FileReadAllBytes(string filePath)
        {
            return _fileSystem.File.ReadAllBytes(filePath);
        }

        public void FileWriteAllBytes(string filePath, byte[] data)
        {
            _fileSystem.File.WriteAllBytes(filePath, data);
        }

        public bool FileExists(string filePath)
        {
            return _fileSystem.File.Exists(filePath);
        }

        public void DeleteFile(string filePath)
        {
            _fileSystem.File.Delete(filePath);
        }

        public string CombinePath(params string[] paths)
        {
            return _fileSystem.Path.Combine(paths);
        }
    }
}
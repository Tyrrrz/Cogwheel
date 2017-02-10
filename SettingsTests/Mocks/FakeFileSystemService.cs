using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tyrrrz.Settings;
using Tyrrrz.Settings.Services;

namespace Settings.Tests.Mocks
{
    public class FakeFileSystemService : IFileSystemService
    {
        public static FakeFileSystemService Instance { get; } = new FakeFileSystemService();

        private readonly List<string> _dirs = new List<string>();
        private readonly Dictionary<string, byte[]> _files = new Dictionary<string, byte[]>();

        public void CreateDirectory(string dirPath)
        {
            if (!_dirs.Contains(dirPath))
                _dirs.Add(dirPath);
        }

        public string GetDirectoryLocation(StorageSpace storageSpace)
        {
            return storageSpace.ToString();
        }

        public bool DirectoryExists(string dirPath)
        {
            return _dirs.Contains(dirPath);
        }

        public void DeleteDirectory(string dirPath, bool recursive)
        {
            _dirs.Remove(dirPath);
            if (recursive)
            {
                foreach (string file in _files.Where(kvp => kvp.Key.StartsWith(dirPath)).Select(kvp => kvp.Key).ToArray())
                    _files.Remove(file);
            }
        }

        public byte[] FileReadAllBytes(string filePath)
        {
            return _files[filePath];
        }

        public void FileWriteAllBytes(string filePath, byte[] data)
        {
            if (_dirs.Contains(Path.GetDirectoryName(filePath)))
                _files[filePath] = data;
        }

        public bool FileExists(string filePath)
        {
            return _files.ContainsKey(filePath);
        }

        public void DeleteFile(string filePath)
        {
            _files.Remove(filePath);
        }

        public string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }
    }
}
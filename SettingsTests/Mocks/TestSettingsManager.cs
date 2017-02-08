using System;
using Tyrrrz.Settings;

namespace Settings.Tests.Mocks
{
    public class TestSettingsManager : SettingsManager
    {
        public int Int { get; set; } = 5;
        public string Str { get; set; } = "Hello World";
        public double Double { get; set; }
        public DateTime DateTime { get; set; }
        public TestEnum Enum { get; set; } = TestEnum.Two;
        public TestClass Class { get; set; }
        public ushort[] Array { get; set; } = {3, 14, 22};

        public TestSettingsManager()
        {
            Configuration.FileStorageSpace = StorageSpace.Instance;
            Configuration.SubDirectoryPath = "test";
            Configuration.FileName = "test.dat";
        }
    }
}

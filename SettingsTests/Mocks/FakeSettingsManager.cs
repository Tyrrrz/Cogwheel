using System;
using Tyrrrz.Settings;

namespace Settings.Tests.Mocks
{
    public class FakeSettingsManager : SettingsManager
    {
        private int _int = 5;
        private string _str = "Hello World";
        private double _double;
        private DateTime _dateTime;
        private FakeEnum _enum = FakeEnum.Two;
        private FakeClass _class;
        private ushort[] _array = {3, 14, 22};

        public int Int
        {
            get { return _int; }
            set { Set(ref _int, value); }
        }

        public string Str
        {
            get { return _str; }
            set { Set(ref _str, value); }
        }

        public double Double
        {
            get { return _double; }
            set { Set(ref _double, value); }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { Set(ref _dateTime, value); }
        }

        public FakeEnum Enum
        {
            get { return _enum; }
            set { Set(ref _enum, value); }
        }

        public FakeClass Class
        {
            get { return _class; }
            set { Set(ref _class, value); }
        }

        public ushort[] Array
        {
            get { return _array; }
            set { Set(ref _array, value); }
        }

        public FakeSettingsManager()
        {
            Configuration.FileSystemService = FakeFileSystemService.Instance;
            Configuration.StorageSpace = StorageSpace.Instance;
            Configuration.SubDirectoryPath = "test";
            Configuration.FileName = "test.dat";
        }
    }
}

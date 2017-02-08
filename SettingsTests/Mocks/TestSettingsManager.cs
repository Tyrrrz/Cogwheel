using System;
using Tyrrrz.Settings;

namespace Settings.Tests.Mocks
{
    public class TestSettingsManager : SettingsManager
    {
        private int _int = 5;
        private string _str = "Hello World";
        private double _double;
        private DateTime _dateTime;
        private TestEnum _enum = TestEnum.Two;
        private TestClass _class;
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

        public TestEnum Enum
        {
            get { return _enum; }
            set { Set(ref _enum, value); }
        }

        public TestClass Class
        {
            get { return _class; }
            set { Set(ref _class, value); }
        }

        public ushort[] Array
        {
            get { return _array; }
            set { Set(ref _array, value); }
        }

        public TestSettingsManager()
        {
            Configuration.FileStorageSpace = StorageSpace.Instance;
            Configuration.SubDirectoryPath = "test";
            Configuration.FileName = "test.dat";
        }
    }
}

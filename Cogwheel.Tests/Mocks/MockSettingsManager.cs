using System;

namespace Cogwheel.Tests.Mocks
{
    public class MockSettingsManager : SettingsManager
    {
        private int _int = 5;
        private string? _str = "Hello World";
        private double _double;
        private DateTime _dateTime;
        private MockEnum _enum = MockEnum.Two;
        private MockClass? _class;
        private ushort[] _array = {3, 14, 22};

        public int Int
        {
            get => _int;
            set => Set(ref _int, value);
        }

        public string? Str
        {
            get => _str;
            set => Set(ref _str, value);
        }

        public double Double
        {
            get => _double;
            set => Set(ref _double, value);
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => Set(ref _dateTime, value);
        }

        public MockEnum Enum
        {
            get => _enum;
            set => Set(ref _enum, value);
        }

        public MockClass? Class
        {
            get => _class;
            set => Set(ref _class, value);
        }

        public ushort[] Array
        {
            get => _array;
            set => Set(ref _array, value);
        }

        public MockSettingsManager()
        {
            Configuration.StorageSpace = StorageSpace.Instance;
            Configuration.SubDirectoryPath = "TestSettings";
            Configuration.FileName = "Config.dat";
        }
    }
}
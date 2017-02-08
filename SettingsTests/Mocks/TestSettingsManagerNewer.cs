namespace Settings.Tests.Mocks
{
    public class TestSettingsManagerNewer : TestSettingsManager
    {
        private char _char = 'Q';

        public char Char
        {
            get { return _char; }
            set { Set(ref _char, value); }
        }
    }
}

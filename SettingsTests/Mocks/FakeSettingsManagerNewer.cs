namespace Settings.Tests.Mocks
{
    public class FakeSettingsManagerNewer : FakeSettingsManager
    {
        private char _char = 'Q';

        public char Char
        {
            get { return _char; }
            set { Set(ref _char, value); }
        }
    }
}

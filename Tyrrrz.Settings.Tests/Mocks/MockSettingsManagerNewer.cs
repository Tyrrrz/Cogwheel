namespace Tyrrrz.Settings.Tests.Mocks
{
    public class MockSettingsManagerNewer : MockSettingsManager
    {
        private char _char = 'Q';

        public char Char
        {
            get => _char;
            set => Set(ref _char, value);
        }
    }
}

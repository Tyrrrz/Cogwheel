namespace Cogwheel.Tests.Fakes;

internal class FakeSettingsWithNullableValue(string filePath) : SettingsBase(filePath)
{
    public int? NullableIntProperty { get; set; }
}

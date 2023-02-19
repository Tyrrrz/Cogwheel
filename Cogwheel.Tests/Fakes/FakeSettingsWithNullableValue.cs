namespace Cogwheel.Tests.Fakes;

internal class FakeSettingsWithNullableValue : SettingsBase
{
    public int? NullableIntProperty { get; set; }

    public FakeSettingsWithNullableValue(string filePath) : base(filePath)
    {
    }
}
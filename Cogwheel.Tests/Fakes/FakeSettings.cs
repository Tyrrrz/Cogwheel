namespace Cogwheel.Tests.Fakes;

internal class FakeSettings(string filePath) : SettingsBase(filePath)
{
    public int IntProperty { get; set; }

    public bool BoolProperty { get; set; }

    public string? StringProperty { get; set; }

    public string? StringPropertyWithDefaultValue { get; set; } = "Default value";
}

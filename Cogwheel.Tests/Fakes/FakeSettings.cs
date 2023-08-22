namespace Cogwheel.Tests.Fakes;

internal class FakeSettings : SettingsBase
{
    public int IntProperty { get; set; }

    public bool BoolProperty { get; set; }

    public string? StringProperty { get; set; }

    public string? StringPropertyWithDefaultValue { get; set; } = "Default value";

    public FakeSettings(string filePath)
        : base(filePath) { }
}

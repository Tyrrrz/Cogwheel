namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomEnum(string filePath) : SettingsBase(filePath)
{
    public CustomEnum CustomEnumProperty { get; set; }
}

public partial class FakeSettingsWithCustomEnum
{
    public enum CustomEnum
    {
        Foo,
        Bar
    }
}

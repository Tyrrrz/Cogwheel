namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomEnum : SettingsBase
{
    public CustomEnum CustomEnumProperty { get; set; }

    public FakeSettingsWithCustomEnum(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomEnum
{
    public enum CustomEnum
    {
        Foo,
        Bar
    }
}
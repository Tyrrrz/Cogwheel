namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomEnum2(string filePath) : SettingsBase(filePath)
{
    public CustomEnum CustomEnumProperty { get; set; }
}

public partial class FakeSettingsWithCustomEnum2
{
    public enum CustomEnum
    {
        Bar,
        Foo,
    }
}

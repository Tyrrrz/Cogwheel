namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomClass(string filePath) : SettingsBase(filePath)
{
    public CustomClass? CustomClassProperty { get; set; }
}

public partial class FakeSettingsWithCustomClass
{
    public class CustomClass
    {
        public int IntProperty { get; set; }

        public string? StringProperty { get; set; }
    }
}

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomClass : SettingsBase
{
    public CustomClass? CustomClassProperty { get; set; }

    public FakeSettingsWithCustomClass(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomClass
{
    public class CustomClass
    {
        public int IntProperty { get; set; }
        public string? StringProperty { get; set; }
    }
}
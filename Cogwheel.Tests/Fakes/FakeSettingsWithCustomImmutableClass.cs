namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomImmutableClass(string filePath) : SettingsBase(filePath)
{
    public CustomClass? CustomClassProperty { get; set; }
}

public partial class FakeSettingsWithCustomImmutableClass
{
    public class CustomClass(int intProperty, string stringProperty)
    {
        public int IntProperty { get; } = intProperty;

        public string StringProperty { get; } = stringProperty;
    }
}

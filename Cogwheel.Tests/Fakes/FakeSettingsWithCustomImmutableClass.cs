namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomImmutableClass : SettingsBase
{
    public CustomClass? CustomClassProperty { get; set; }

    public FakeSettingsWithCustomImmutableClass(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomImmutableClass
{
    public class CustomClass
    {
        public int IntProperty { get; }
        public string StringProperty { get; }

        public CustomClass(int intProperty, string stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }
    }
}
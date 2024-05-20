namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomImmutableStruct(string filePath) : SettingsBase(filePath)
{
    public CustomStruct? CustomStructProperty { get; set; }
}

public partial class FakeSettingsWithCustomImmutableStruct
{
    public readonly struct CustomStruct(int intProperty, string stringProperty)
    {
        public int IntProperty { get; } = intProperty;

        public string StringProperty { get; } = stringProperty;
    }
}

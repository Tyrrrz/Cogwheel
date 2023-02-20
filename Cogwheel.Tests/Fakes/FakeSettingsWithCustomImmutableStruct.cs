namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomImmutableStruct: SettingsBase
{
    public CustomStruct? CustomStructProperty { get; set; }

    public FakeSettingsWithCustomImmutableStruct(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomImmutableStruct
{
    public readonly struct CustomStruct
    {
        public int IntProperty { get; }

        public string StringProperty { get; }

        public CustomStruct(int intProperty, string stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }
    }
}
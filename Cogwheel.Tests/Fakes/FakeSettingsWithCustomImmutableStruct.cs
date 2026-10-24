using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomImmutableStruct(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomImmutableStructSerializerContext.Default)
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

public partial class FakeSettingsWithCustomImmutableStruct
{
    [JsonSerializable(typeof(FakeSettingsWithCustomImmutableStruct))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomImmutableStructSerializerContext
        : JsonSerializerContext;
}

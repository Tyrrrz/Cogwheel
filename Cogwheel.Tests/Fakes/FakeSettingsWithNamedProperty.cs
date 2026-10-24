using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithNamedProperty(string filePath)
    : SettingsBase(filePath, FakeSettingsWithNamedPropertySerializerContext.Default)
{
    [JsonPropertyName("foo")]
    public int IntProperty { get; set; }
}

public partial class FakeSettingsWithNamedProperty
{
    [JsonSerializable(typeof(FakeSettingsWithNamedProperty))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithNamedPropertySerializerContext : JsonSerializerContext;
}

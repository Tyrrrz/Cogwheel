using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithIgnoredProperty(string filePath)
    : SettingsBase(filePath, FakeSettingsWithIgnoredPropertySerializerContext.Default)
{
    public int IntProperty { get; set; }

    [JsonIgnore]
    public string? IgnoredProperty { get; set; }
}

public partial class FakeSettingsWithIgnoredProperty
{
    [JsonSerializable(typeof(FakeSettingsWithIgnoredProperty))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithIgnoredPropertySerializerContext : JsonSerializerContext;
}

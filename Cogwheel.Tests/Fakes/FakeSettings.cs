using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

internal partial class FakeSettings(string filePath)
    : SettingsBase(filePath, FakeSettingsSerializerContext.Default)
{
    public int IntProperty { get; set; }

    public bool BoolProperty { get; set; }

    public string? StringProperty { get; set; }

    public string? StringPropertyWithDefaultValue { get; set; } = "Default value";
}

internal partial class FakeSettings
{
    [JsonSerializable(typeof(FakeSettings))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsSerializerContext : JsonSerializerContext;
}

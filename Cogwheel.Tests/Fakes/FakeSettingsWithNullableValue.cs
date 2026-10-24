using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

internal partial class FakeSettingsWithNullableValue(string filePath)
    : SettingsBase(filePath, FakeSettingsWithNullableValueSerializerContext.Default)
{
    public int? NullableIntProperty { get; set; }
}

internal partial class FakeSettingsWithNullableValue
{
    [JsonSerializable(typeof(FakeSettingsWithNullableValue))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithNullableValueSerializerContext : JsonSerializerContext;
}

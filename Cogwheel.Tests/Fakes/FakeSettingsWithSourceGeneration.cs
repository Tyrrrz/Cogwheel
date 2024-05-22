using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

internal partial class FakeSettingsWithSourceGeneration(string filePath)
    : SettingsBase(filePath, SerializerContext.Default)
{
    public int IntProperty { get; set; }

    public bool BoolProperty { get; set; }

    public string? StringProperty { get; set; }
}

internal partial class FakeSettingsWithSourceGeneration
{
    [JsonSerializable(typeof(FakeSettingsWithSourceGeneration))]
    private partial class SerializerContext : JsonSerializerContext;
}

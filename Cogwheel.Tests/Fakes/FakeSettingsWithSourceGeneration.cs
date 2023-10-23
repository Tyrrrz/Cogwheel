using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

internal partial class FakeSettingsWithSourceGeneration : SettingsBase
{
    public int IntProperty { get; set; }

    public bool BoolProperty { get; set; }

    public string? StringProperty { get; set; }

    public FakeSettingsWithSourceGeneration(string filePath)
        : base(filePath, new JsonSerializerOptions { TypeInfoResolver = SerializerContext.Default })
    { }
}

internal partial class FakeSettingsWithSourceGeneration
{
    [JsonSerializable(typeof(FakeSettingsWithSourceGeneration))]
    private partial class SerializerContext : JsonSerializerContext { }
}

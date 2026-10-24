using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomRecord(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomRecordSerializerContext.Default)
{
    public CustomRecord? CustomRecordProperty { get; set; }
}

public partial class FakeSettingsWithCustomRecord
{
    public record CustomRecord(int IntProperty, string StringProperty);
}

public partial class FakeSettingsWithCustomRecord
{
    [JsonSerializable(typeof(FakeSettingsWithCustomRecord))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomRecordSerializerContext : JsonSerializerContext;
}

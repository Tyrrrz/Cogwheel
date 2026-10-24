using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomStructRecord(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomStructRecordSerializerContext.Default)
{
    public CustomRecord CustomRecordProperty { get; set; }
}

public partial class FakeSettingsWithCustomStructRecord
{
    public record struct CustomRecord(int IntProperty, string StringProperty);
}

public partial class FakeSettingsWithCustomStructRecord
{
    [JsonSerializable(typeof(FakeSettingsWithCustomStructRecord))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomStructRecordSerializerContext
        : JsonSerializerContext;
}

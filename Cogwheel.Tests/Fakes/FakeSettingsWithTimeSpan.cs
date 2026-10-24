using System;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithTimeSpan(string filePath)
    : SettingsBase(filePath, FakeSettingsWithTimeSpanSerializerContext.Default)
{
    public TimeSpan TimeSpanProperty { get; set; }
}

public partial class FakeSettingsWithTimeSpan
{
    [JsonSerializable(typeof(FakeSettingsWithTimeSpan))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithTimeSpanSerializerContext : JsonSerializerContext;
}

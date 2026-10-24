using System;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithDateTimeOffset(string filePath)
    : SettingsBase(filePath, FakeSettingsWithDateTimeOffsetSerializerContext.Default)
{
    public DateTimeOffset DateTimeOffsetProperty { get; set; }
}

public partial class FakeSettingsWithDateTimeOffset
{
    [JsonSerializable(typeof(FakeSettingsWithDateTimeOffset))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithDateTimeOffsetSerializerContext : JsonSerializerContext;
}

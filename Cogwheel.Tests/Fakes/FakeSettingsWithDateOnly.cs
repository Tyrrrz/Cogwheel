using System;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithDateOnly(string filePath)
    : SettingsBase(filePath, FakeSettingsWithDateOnlySerializerContext.Default)
{
    public DateOnly DateOnlyProperty { get; set; }
}

public partial class FakeSettingsWithDateOnly
{
    [JsonSerializable(typeof(FakeSettingsWithDateOnly))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithDateOnlySerializerContext : JsonSerializerContext;
}

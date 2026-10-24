using System;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithTimeOnly(string filePath)
    : SettingsBase(filePath, FakeSettingsWithTimeOnlySerializerContext.Default)
{
    public TimeOnly TimeOnlyProperty { get; set; }
}

public partial class FakeSettingsWithTimeOnly
{
    [JsonSerializable(typeof(FakeSettingsWithTimeOnly))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithTimeOnlySerializerContext : JsonSerializerContext;
}

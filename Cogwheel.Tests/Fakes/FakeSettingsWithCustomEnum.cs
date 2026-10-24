using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomEnum(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomEnumSerializerContext.Default)
{
    public CustomEnum CustomEnumProperty { get; set; }
}

public partial class FakeSettingsWithCustomEnum
{
    public enum CustomEnum
    {
        Foo,
        Bar,
    }
}

public partial class FakeSettingsWithCustomEnum
{
    [JsonSerializable(typeof(FakeSettingsWithCustomEnum))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomEnumSerializerContext : JsonSerializerContext;
}

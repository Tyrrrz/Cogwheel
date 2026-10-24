using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomEnum2(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomEnum2SerializerContext.Default)
{
    public CustomEnum CustomEnumProperty { get; set; }
}

public partial class FakeSettingsWithCustomEnum2
{
    public enum CustomEnum
    {
        Bar,
        Foo,
    }
}

public partial class FakeSettingsWithCustomEnum2
{
    [JsonSerializable(typeof(FakeSettingsWithCustomEnum2))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomEnum2SerializerContext : JsonSerializerContext;
}

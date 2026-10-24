using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomClass(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomClassSerializerContext.Default)
{
    public CustomClass? CustomClassProperty { get; set; }
}

public partial class FakeSettingsWithCustomClass
{
    public class CustomClass
    {
        public int IntProperty { get; set; }

        public string? StringProperty { get; set; }
    }
}

public partial class FakeSettingsWithCustomClass
{
    [JsonSerializable(typeof(FakeSettingsWithCustomClass))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomClassSerializerContext : JsonSerializerContext;
}

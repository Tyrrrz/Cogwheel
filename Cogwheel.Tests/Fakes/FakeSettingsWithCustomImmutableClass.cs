using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomImmutableClass(string filePath)
    : SettingsBase(filePath, FakeSettingsWithCustomImmutableClassSerializerContext.Default)
{
    public CustomClass? CustomClassProperty { get; set; }
}

public partial class FakeSettingsWithCustomImmutableClass
{
    public class CustomClass(int intProperty, string stringProperty)
    {
        public int IntProperty { get; } = intProperty;

        public string StringProperty { get; } = stringProperty;
    }
}

public partial class FakeSettingsWithCustomImmutableClass
{
    [JsonSerializable(typeof(FakeSettingsWithCustomImmutableClass))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class FakeSettingsWithCustomImmutableClassSerializerContext
        : JsonSerializerContext;
}

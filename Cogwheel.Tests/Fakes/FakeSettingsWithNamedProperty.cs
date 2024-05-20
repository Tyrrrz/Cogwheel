using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithNamedProperty(string filePath) : SettingsBase(filePath)
{
    [JsonPropertyName("foo")]
    public int IntProperty { get; set; }
}

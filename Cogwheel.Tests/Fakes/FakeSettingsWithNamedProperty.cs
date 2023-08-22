using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithNamedProperty : SettingsBase
{
    [JsonPropertyName("foo")]
    public int IntProperty { get; set; }

    public FakeSettingsWithNamedProperty(string filePath)
        : base(filePath) { }
}

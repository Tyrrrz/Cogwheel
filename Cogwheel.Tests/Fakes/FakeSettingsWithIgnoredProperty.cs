using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithIgnoredProperty(string filePath) : SettingsBase(filePath)
{
    public int IntProperty { get; set; }

    [JsonIgnore]
    public string? IgnoredProperty { get; set; }
}

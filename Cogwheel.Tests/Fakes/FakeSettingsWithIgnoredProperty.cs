using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithIgnoredProperty : SettingsBase
{
    public int IntProperty { get; set; }

    [JsonIgnore]
    public string? IgnoredProperty { get; set; }

    public FakeSettingsWithIgnoredProperty(string filePath) : base(filePath)
    {
    }
}
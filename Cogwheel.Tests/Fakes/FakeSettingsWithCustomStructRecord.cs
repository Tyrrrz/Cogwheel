namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomStructRecord(string filePath) : SettingsBase(filePath)
{
    public CustomRecord CustomRecordProperty { get; set; }
}

public partial class FakeSettingsWithCustomStructRecord
{
    public record struct CustomRecord(int IntProperty, string StringProperty);
}

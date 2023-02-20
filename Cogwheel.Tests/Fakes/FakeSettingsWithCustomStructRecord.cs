namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomStructRecord : SettingsBase
{
    public CustomRecord CustomRecordProperty { get; set; }

    public FakeSettingsWithCustomStructRecord(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomStructRecord
{
    public record struct CustomRecord(int IntProperty, string StringProperty);
}
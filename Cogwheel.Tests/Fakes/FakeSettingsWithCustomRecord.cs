namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomRecord(string filePath) : SettingsBase(filePath)
{
    public CustomRecord? CustomRecordProperty { get; set; }
}

public partial class FakeSettingsWithCustomRecord
{
    public record CustomRecord(int IntProperty, string StringProperty);
}

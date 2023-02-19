namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomRecord : SettingsBase
{
    public CustomRecord? CustomRecordProperty { get; set; }

    public FakeSettingsWithCustomRecord(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomRecord
{
    public record CustomRecord(int IntProperty, string StringProperty);
}
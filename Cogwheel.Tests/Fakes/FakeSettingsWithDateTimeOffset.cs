using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithDateTimeOffset : SettingsBase
{
    public DateTimeOffset DateTimeOffsetProperty { get; set; }

    public FakeSettingsWithDateTimeOffset(string filePath)
        : base(filePath) { }
}

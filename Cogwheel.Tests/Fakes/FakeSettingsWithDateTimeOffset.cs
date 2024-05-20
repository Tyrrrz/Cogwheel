using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithDateTimeOffset(string filePath) : SettingsBase(filePath)
{
    public DateTimeOffset DateTimeOffsetProperty { get; set; }
}

using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithTimeSpan(string filePath) : SettingsBase(filePath)
{
    public TimeSpan TimeSpanProperty { get; set; }
}

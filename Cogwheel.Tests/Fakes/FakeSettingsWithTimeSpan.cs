using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithTimeSpan : SettingsBase
{
    public TimeSpan TimeSpanProperty { get; set; }

    public FakeSettingsWithTimeSpan(string filePath)
        : base(filePath) { }
}

using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithDateOnly : SettingsBase
{
    public DateOnly DateOnlyProperty { get; set; }

    public FakeSettingsWithDateOnly(string filePath) : base(filePath)
    {
    }
}
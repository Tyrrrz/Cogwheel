using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithDateOnly(string filePath) : SettingsBase(filePath)
{
    public DateOnly DateOnlyProperty { get; set; }
}

using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithTimeOnly(string filePath) : SettingsBase(filePath)
{
    public TimeOnly TimeOnlyProperty { get; set; }
}

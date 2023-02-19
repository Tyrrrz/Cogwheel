using System;

namespace Cogwheel.Tests.Fakes;

public class FakeSettingsWithTimeOnly : SettingsBase
{
    public TimeOnly TimeOnlyProperty { get; set; }

    public FakeSettingsWithTimeOnly(string filePath) : base(filePath)
    {
    }
}
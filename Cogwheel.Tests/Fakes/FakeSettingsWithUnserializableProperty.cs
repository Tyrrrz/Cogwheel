using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithUnserializableProperty : SettingsBase
{
    [JsonConverter(typeof(BrokenJsonConverter))]
    public CustomClass? UnserializableProperty { get; set; }

    public FakeSettingsWithUnserializableProperty(string filePath)
        : base(filePath) { }
}

public partial class FakeSettingsWithUnserializableProperty
{
    public class CustomClass
    {
        public string? Foo { get; init; }
    }
}

public partial class FakeSettingsWithUnserializableProperty
{
    private class BrokenJsonConverter : JsonConverter<CustomClass>
    {
        public override CustomClass Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        ) => throw new Exception("Expected exception.");

        public override void Write(
            Utf8JsonWriter writer,
            CustomClass value,
            JsonSerializerOptions options
        ) => throw new Exception("Expected exception.");
    }
}

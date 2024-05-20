using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomConverterProperty(string filePath)
    : SettingsBase(filePath)
{
    [JsonConverter(typeof(CustomJsonConverter))]
    public CustomClass? CustomConverterProperty { get; set; }
}

public partial class FakeSettingsWithCustomConverterProperty
{
    public class CustomClass
    {
        // Using a private setter here ensures that the property is not
        // settable without using a custom converter.
        public string? Value { get; private set; }

        public void Set(string? value) => Value = value;
    }
}

public partial class FakeSettingsWithCustomConverterProperty
{
    private class CustomJsonConverter : JsonConverter<CustomClass>
    {
        public override CustomClass Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var result = new CustomClass();

            result.Set(reader.GetString());

            return result;
        }

        public override void Write(
            Utf8JsonWriter writer,
            CustomClass value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(value.Value);
    }
}

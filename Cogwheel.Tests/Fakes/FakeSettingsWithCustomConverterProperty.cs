using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomConverterProperty : SettingsBase
{
    [JsonConverter(typeof(CustomJsonConverter))]
    public CustomClass? CustomConverterProperty { get; set; }

    public FakeSettingsWithCustomConverterProperty(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomConverterProperty
{
    public class CustomClass
    {
        // Using an internal setter here ensures that the property is not
        // settable without using a custom converter.
        public string? Foo { get; internal init; }
    }
}

public partial class FakeSettingsWithCustomConverterProperty
{
    private class CustomJsonConverter : JsonConverter<CustomClass>
    {
        public override CustomClass? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            CustomClass? result = null;

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName &&
                        reader.GetString() == "xyz" &&
                        reader.Read() &&
                        reader.TokenType == JsonTokenType.String)
                    {
                        var foo = reader.GetString();
                        result = new CustomClass
                        {
                            Foo = foo
                        };
                    }
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, CustomClass value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("xyz", value.Foo);
            writer.WriteEndObject();
        }
    }
}
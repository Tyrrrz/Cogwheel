using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cogwheel.Tests.Fakes;

public partial class FakeSettingsWithCustomConverterProperty : SettingsBase
{
    [JsonConverter(typeof(MyJsonConverter))]
    public MyClass? ConvertibleProperty { get; set; }

    public FakeSettingsWithCustomConverterProperty(string filePath) : base(filePath)
    {
    }
}

public partial class FakeSettingsWithCustomConverterProperty
{
    public class MyClass
    {
        // Using an internal setter here ensures that the property is not
        // settable without using a custom converter.
        public string? Foo { get; internal init; }
    }
}

public partial class FakeSettingsWithCustomConverterProperty
{
    private class MyJsonConverter : JsonConverter<MyClass>
    {
        public override MyClass? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? foo = null;

            if (reader.TokenType != JsonTokenType.StartObject)
                return null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                var propertyName = reader.GetString();
                if (propertyName is null)
                    continue;

                reader.Read();

                switch (propertyName)
                {
                    case "xyz":
                        foo = reader.GetString();
                        break;
                }
            }

            return foo is not null
                ? new MyClass { Foo = foo }
                : null;
        }

        public override void Write(Utf8JsonWriter writer, MyClass value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("xyz", value.Foo);
            writer.WriteEndObject();
        }
    }
}
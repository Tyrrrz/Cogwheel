using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cogwheel;

/// <summary>
/// Base class for settings.
/// </summary>
public abstract class SettingsBase
{
    private readonly string _filePath;

    private readonly IReadOnlyList<PropertyInfo> _properties;
    private readonly IReadOnlyDictionary<PropertyInfo, object?> _defaults;

    /// <summary>
    /// Initializes an instance of <see cref="SettingsBase" />.
    /// </summary>
    protected SettingsBase(string filePath)
    {
        _filePath = filePath;

        _properties = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.DeclaringType != typeof(SettingsBase))
            .Where(p => p.CanRead && p.CanWrite)
            .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() is null)
            .ToArray();

        // Default values for properties are initialized before the constructor is called,
        // so we can safely retrieve them here.
        _defaults = _properties.ToDictionary(p => p, p => p.GetValue(this));
    }

    /// <summary>
    /// Resets the settings to their default values.
    /// </summary>
    public virtual void Reset()
    {
        foreach (var property in _properties)
            property.SetValue(this, _defaults[property]);
    }

    /// <summary>
    /// Saves the settings to file.
    /// </summary>
    public virtual void Save()
    {
        var dirPath = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(dirPath))
            Directory.CreateDirectory(dirPath);

        using var stream = File.Create(_filePath);
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Indented = true
        });

        writer.WriteStartObject();

        // Write metadata
        {
            writer.WriteCommentValue($"Generated on {DateTimeOffset.Now:u}");

            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly is not null)
            {
                writer.WriteCommentValue(
                    $"{entryAssembly.GetName().Name} v{entryAssembly.GetName().Version}"
                );
            }

            var thisAssembly = Assembly.GetExecutingAssembly();
            writer.WriteCommentValue(
                $"{thisAssembly.GetName().Name} v{thisAssembly.GetName().Version}"
            );
        }

        // Write settings
        foreach (var property in _properties)
        {
            writer.WritePropertyName(
                property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ??
                property.Name
            );

            JsonSerializer.Serialize(writer, property.GetValue(this), property.PropertyType);
        }

        writer.WriteEndObject();
    }

    /// <summary>
    /// Loads the settings from file.
    /// Returns true if the file was loaded, false if it didn't exist.
    /// </summary>
    public virtual bool Load()
    {
        try
        {
            using var stream = File.OpenRead(_filePath);
            using var document = JsonDocument.Parse(stream, new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            });

            foreach (var jsonProperty in document.RootElement.EnumerateObject())
            {
                var property = _properties.FirstOrDefault(p => string.Equals(
                    p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? p.Name,
                    jsonProperty.Name,
                    StringComparison.Ordinal
                ));

                property?.SetValue(
                    this,
                    JsonSerializer.Deserialize(jsonProperty.Value.GetRawText(), property.PropertyType)
                );
            }

            return true;
        }
        catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes the settings file.
    /// Returns true if the file was deleted, false if it didn't exist.
    /// </summary>
    public virtual bool Delete()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                // This doesn't throw if the file doesn't exist, but
                // does throw if the directory doesn't exist.
                File.Delete(_filePath);
            }
            else
            {
                return false;
            }

            return true;
        }
        catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
        {
            return false;
        }
    }
}
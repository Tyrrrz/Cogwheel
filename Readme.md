# Cogwheel

[![Status](https://img.shields.io/badge/status-active-47c219.svg)](https://github.com/Tyrrrz/.github/blob/prime/docs/project-status.md)
[![Build](https://img.shields.io/github/actions/workflow/status/Tyrrrz/Cogwheel/main.yml?branch=prime)](https://github.com/Tyrrrz/Cogwheel/actions)
[![Coverage](https://img.shields.io/codecov/c/github/Tyrrrz/Cogwheel/prime)](https://codecov.io/gh/Tyrrrz/Cogwheel)
[![Version](https://img.shields.io/nuget/v/Cogwheel.svg)](https://nuget.org/packages/Cogwheel)
[![Downloads](https://img.shields.io/nuget/dt/Cogwheel.svg)](https://nuget.org/packages/Cogwheel)
[![Discord](https://img.shields.io/discord/869237470565392384?label=discord)](https://discord.gg/2SUWKFnHSm)

<table>
    <tr>
        <td width="99999" align="center">Development of this project is entirely funded by the community. <b><a href="https://tyrrrz.me/donate">Consider donating to support!</a></b></td>
    </tr>
</table>

<p align="center">
    <img src="favicon.png" alt="Icon" />
</p>

> **Made in 🇺🇦 Ukraine — [despite Russia's best efforts](https://tyrrrz.me/ukraine)**.
> Using this project or its source code comes with the implicit agreement that you condemn Russia's war of aggression against Ukraine, recognize Russia as a terrorist state, and support Ukraine's territorial integrity.

**Cogwheel** (formerly [**Tyrrrz.Settings**](https://nuget.org/packages/Tyrrrz.Settings)) is a simple library for storing and retrieving user settings in applications.
It serves as a replacement for the built-in [`System.Configuration.SettingsBase`](https://learn.microsoft.com/dotnet/api/system.configuration.settingsbase) class, and offers more customization and flexibility.

## Install

- 📦 [NuGet](https://nuget.org/packages/Cogwheel): `dotnet add package Cogwheel`

## Usage

To define your own application settings, create a class that inherits from `SettingsBase`:

```csharp
using Cogwheel;

public class MySettings() : SettingsBase("path/to/settings.json")
{
    public string StringSetting { get; set; } = "foo";

    public int IntSetting { get; set; } = 42;
}
```

Using an instance of this class, you can load, modify, and save settings:

```csharp
var settings = new MySettings();

settings.Load();

settings.StringSetting = "bar";
settings.IntSetting = 1337;

settings.Save();
```

You can also restore settings to their default values:

```csharp
var settings = new MySettings();

settings.StringSetting = "bar";
settings.IntSetting = 1337;

settings.Reset();

// settings.StringSetting == "foo"
// settings.IntSetting == 42
```


### Customizing behavior

Under the hood, **Cogwheel** uses [`System.Text.Json`](https://docs.microsoft.com/dotnet/api/system.text.json) to serialize and deserialize settings.
You can use various attributes defined in that namespace to customize the serialization behavior:

```csharp
using Cogwheel;
using System.Text.Json.Serialization;

public class MySettings() : SettingsBase("path/to/settings.json")
{
    [JsonPropertyName("string_setting")]
    public string StringSetting { get; set; } = "foo";

    [JsonIgnore]
    public int IntSetting { get; set; } = 42;
}
```

You can also provide a custom `JsonSerializerOptions` instance to further customize the serialization process:

```csharp
using Cogwheel;
using System.Text.Json;

public class MySettings() : SettingsBase(
    "path/to/settings.json",
    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
)
{
    public string StringSetting { get; set; } = "foo";

    public int IntSetting { get; set; } = 42;
}
```

### Static serialization

If you wish to use the underlying `System.Text.Json` serialization engine in its static mode rather than rely on reflection, you need to pass a valid instance of `IJsonTypeInfoResolver` to the `SettingsBase` constructor.
You can leverage the built-in source generator to create one for you by defining a `partial` class derived from `JsonSerializerContext` as shown below:


```csharp
using Cogwheel;
using System.Text.Json.Serialization;

public partial class MySettings() : SettingsBase(
    "path/to/settings.json",
    MySettingsSerializerContext.Default
    // Or:
    // new JsonSerializerOptions { TypeInfoResolver = MySettingsSerializerContext.Default }
)
{
    public string StringSetting { get; set; } = "foo";

    public int IntSetting { get; set; } = 42;
}

// Keep serialization context in a separate partial class for organization
public partial class MySettings
{
    // Define a custom JSON serialization context for source-generated code
    [JsonSerializable(typeof(MySettings))]
    // Recommended: serialize enums as strings for better readability
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    private partial class MySettingsSerializerContext : JsonSerializerContext;
}
```

> [!NOTE]
> To learn more about source generation in `System.Text.Json`, see the [official documentation](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/source-generation).

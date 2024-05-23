# Cogwheel

[![Status](https://img.shields.io/badge/status-active-47c219.svg)](https://github.com/Tyrrrz/.github/blob/master/docs/project-status.md)
[![Made in Ukraine](https://img.shields.io/badge/made_in-ukraine-ffd700.svg?labelColor=0057b7)](https://tyrrrz.me/ukraine)
[![Build](https://img.shields.io/github/actions/workflow/status/Tyrrrz/Cogwheel/main.yml?branch=master)](https://github.com/Tyrrrz/Cogwheel/actions)
[![Coverage](https://img.shields.io/codecov/c/github/Tyrrrz/Cogwheel/master)](https://codecov.io/gh/Tyrrrz/Cogwheel)
[![Version](https://img.shields.io/nuget/v/Cogwheel.svg)](https://nuget.org/packages/Cogwheel)
[![Downloads](https://img.shields.io/nuget/dt/Cogwheel.svg)](https://nuget.org/packages/Cogwheel)
[![Discord](https://img.shields.io/discord/869237470565392384?label=discord)](https://discord.gg/2SUWKFnHSm)
[![Fuck Russia](https://img.shields.io/badge/fuck-russia-e4181c.svg?labelColor=000000)](https://twitter.com/tyrrrz/status/1495972128977571848)

<table>
    <tr>
        <td width="99999" align="center">Development of this project is entirely funded by the community. <b><a href="https://tyrrrz.me/donate">Consider donating to support!</a></b></td>
    </tr>
</table>

<p align="center">
    <img src="favicon.png" alt="Icon" />
</p>

**Cogwheel** (formerly [**Tyrrrz.Settings**](https://nuget.org/packages/Tyrrrz.Settings)) is a simple library for storing and retrieving settings in desktop applications.
It serves as a replacement for the built-in [`System.Configuration.SettingsBase`](https://learn.microsoft.com/en-us/dotnet/api/system.configuration.settingsbase) class, and offers more customization and flexibility.

## Terms of use<sup>[[?]](https://github.com/Tyrrrz/.github/blob/master/docs/why-so-political.md)</sup>

By using this project or its source code, for any purpose and in any shape or form, you grant your **implicit agreement** to all the following statements:

- You **condemn Russia and its military aggression against Ukraine**
- You **recognize that Russia is an occupant that unlawfully invaded a sovereign state**
- You **support Ukraine's territorial integrity, including its claims over temporarily occupied territories of Crimea and Donbas**
- You **reject false narratives perpetuated by Russian state propaganda**

To learn more about the war and how you can help, [click here](https://tyrrrz.me/ukraine). Glory to Ukraine! 🇺🇦

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

Under the hood, **Cogwheel** uses [`System.Text.Json`](https://docs.microsoft.com/en-us/dotnet/api/system.text.json) to serialize and deserialize settings.
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

### Compile-time serialization

If you want to use compile-time serialization as opposed to relying on reflection, you need to provide a valid `IJsonTypeInfoResolver` instance.
You can provide it either directly or as part of a `JsonSerializerOptions` instance:

```csharp
using Cogwheel;
using System.Text.Json.Serialization;

public class MySettings() : SettingsBase(
    "path/to/settings.json",
    MyJsonSerializerContext.Default
    // Or:
    // new JsonSerializationOptions { TypeInfoResolver = MyJsonSerializerContext.Default }
)
{
    public string StringSetting { get; set; } = "foo";

    public int IntSetting { get; set; } = 42;
}

// Define a custom JSON serialization context for auto-generated code
[JsonSerializable(typeof(MySettings))]
internal partial class MyJsonSerializerContext : JsonSerializerContext;
```

> **Note**:
> To learn more about compile-time serialization in `System.Text.Json`, see the [official documentation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation).
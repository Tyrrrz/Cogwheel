# Changelog

> **Important**:
> This changelog is no longer maintained and will be removed in the future.
> Going forward, new versions of this package will have the corresponding release notes published on [GitHub Releases](https://github.com/Tyrrrz/Cogwheel/releases).

## v2.0.4 (26-Oct-2023)

- Added a constructor overload for `SettingsBase` that accepts an instance of `JsonSerializerOptions`. Among other things, this lets you provide a custom `IJsonTypeInfoResolver` for compile-time serialization.

## v2.0.3 (18-Jun-2023)

- Fixed an issue where calling `Save()` would leave behind a corrupted file if the underlying serialization process failed. This was most often evident in the form of cryptic parsing-related exceptions on subsequent calls to `Load()`. Now, `Save()` will not create a file if an error occurs during serialization.

## v2.0.2 (27-Apr-2023)

- Improved support for older target frameworks via polyfills.

## v2.0.1 (20-Feb-2023)

- Fixed an issue where custom converters specified by the `[JsonConverter]` were not being used.

## v2.0 (19-Feb-2023)

- Reworked and renamed the library.

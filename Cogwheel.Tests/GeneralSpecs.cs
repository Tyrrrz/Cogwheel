using System.IO;
using Cogwheel.Tests.Fakes;
using Cogwheel.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Cogwheel.Tests;

public class GeneralSpecs
{
    [Fact]
    public void I_can_save_settings()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettings(file.Path);

        // Act
        settings.Save();

        // Assert
        File.Exists(file.Path).Should().BeTrue();
    }

    [Fact]
    public void I_can_load_saved_settings()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettings(file.Path)
        {
            IntProperty = 42,
            BoolProperty = true,
            StringProperty = "foo",
            StringPropertyWithDefaultValue = "bar"
        };

        settings.Save();

        // Act
        var loadedSettings = new FakeSettings(file.Path);
        var wasLoaded = loadedSettings.Load();

        // Assert
        wasLoaded.Should().BeTrue();
        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_attempt_to_load_saved_settings_and_not_get_an_error_if_they_were_not_saved()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettings(file.Path);

        // Act
        var wasLoaded = settings.Load();

        // Assert
        wasLoaded.Should().BeFalse();
    }

    [Fact]
    public void I_can_reset_settings_to_their_defaults()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettings(file.Path)
        {
            IntProperty = 42,
            BoolProperty = true,
            StringProperty = "foo",
            StringPropertyWithDefaultValue = "bar"
        };

        // Act
        settings.Reset();

        // Assert
        settings.Should().BeEquivalentTo(new FakeSettings(file.Path));
    }

    [Fact]
    public void I_can_delete_saved_settings()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettings(file.Path);

        settings.Save();

        // Act
        var wasDeleted = settings.Delete();

        // Assert
        wasDeleted.Should().BeTrue();
        File.Exists(file.Path).Should().BeFalse();
    }

    [Fact]
    public void I_can_attempt_to_delete_saved_settings_and_not_get_an_error_if_they_were_not_saved()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettings(file.Path);

        // Act
        var wasDeleted = settings.Delete();

        // Assert
        wasDeleted.Should().BeFalse();
    }
}
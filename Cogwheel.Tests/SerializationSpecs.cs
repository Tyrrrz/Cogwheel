using System;
using Cogwheel.Tests.Fakes;
using Cogwheel.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Cogwheel.Tests;

public class SerializationSpecs
{
    [Fact]
    public void I_can_define_a_setting_of_a_nullable_value_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithNullableValue(file.Path)
        {
            NullableIntProperty = 42
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithNullableValue(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_nullable_value_type_and_get_null_if_it_is_unset()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithNullableValue(file.Path)
        {
            NullableIntProperty = null
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithNullableValue(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_DateTimeOffset_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithDateTimeOffset(file.Path)
        {
            DateTimeOffsetProperty = new DateTimeOffset(2023, 04, 28, 13, 37, 00, TimeSpan.FromHours(+2))
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithDateTimeOffset(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_TimeSpan_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithTimeSpan(file.Path)
        {
            TimeSpanProperty = TimeSpan.FromMinutes(3.14)
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithTimeSpan(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_TimeOnly_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithTimeOnly(file.Path)
        {
            TimeOnlyProperty = new TimeOnly(23, 59)
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithTimeOnly(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_DateOnly_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithDateOnly(file.Path)
        {
            DateOnlyProperty = new DateOnly(2023, 04, 28)
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithDateOnly(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_custom_enum_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomEnum(file.Path)
        {
            CustomEnumProperty = FakeSettingsWithCustomEnum.CustomEnum.Bar
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomEnum(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_custom_class_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomClass(file.Path)
        {
            CustomClassProperty = new FakeSettingsWithCustomClass.CustomClass
            {
                IntProperty = 42,
                StringProperty = "foo"
            }
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomClass(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_custom_immutable_class_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomImmutableClass(file.Path)
        {
            CustomClassProperty = new FakeSettingsWithCustomImmutableClass.CustomClass(42, "foo")
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomImmutableClass(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact(Skip = "STJ does not support default parameterized constructors in structs")]
    public void I_can_define_a_setting_of_a_custom_immutable_struct_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomImmutableStruct(file.Path)
        {
            CustomStructProperty = new FakeSettingsWithCustomImmutableStruct.CustomStruct(42, "foo")
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomImmutableStruct(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_custom_record_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomRecord(file.Path)
        {
            CustomRecordProperty = new FakeSettingsWithCustomRecord.CustomRecord(42, "foo")
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomRecord(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_of_a_custom_struct_record_type()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomStructRecord(file.Path)
        {
            CustomRecordProperty = new FakeSettingsWithCustomStructRecord.CustomRecord(42, "foo")
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomStructRecord(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_that_gets_serialized_using_a_custom_name()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithNamedProperty(file.Path)
        {
            IntProperty = 42
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithNamedProperty(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_that_gets_serialized_using_a_custom_converter()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithCustomConverterProperty(file.Path)
        {
            CustomConverterProperty = new FakeSettingsWithCustomConverterProperty.CustomClass()
        };

        settings.CustomConverterProperty.Set("foo");

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithCustomConverterProperty(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings);
    }

    [Fact]
    public void I_can_define_a_setting_that_does_not_get_serialized()
    {
        // Arrange
        using var file = TempFile.Create();
        var settings = new FakeSettingsWithIgnoredProperty(file.Path)
        {
            IntProperty = 42,
            IgnoredProperty = "foo"
        };

        // Act
        settings.Save();

        // Assert
        var loadedSettings = new FakeSettingsWithIgnoredProperty(file.Path);
        loadedSettings.Load();

        loadedSettings.Should().BeEquivalentTo(settings, o => o.Excluding(x => x.IgnoredProperty));
        loadedSettings.IgnoredProperty.Should().BeNull();
    }
}
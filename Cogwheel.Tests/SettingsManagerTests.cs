using System;
using System.IO;
using Cogwheel.Tests.Mocks;
using Xunit;

namespace Cogwheel.Tests
{
    public class SettingsManagerTests : IDisposable
    {
        [Fact]
        public void InstantiateTest()
        {
            var manager = new MockSettingsManager();

            // Check that configuration properties are set
            Assert.NotNull(manager.Configuration);
            Assert.NotNull(manager.Configuration.SubDirectoryPath);
            Assert.NotNull(manager.Configuration.FileName);
            Assert.NotNull(manager.FullDirectoryPath);
            Assert.NotNull(manager.FullFilePath);
        }

        [Fact]
        public void CopyFromTest()
        {
            var manager1 = new MockSettingsManager();
            var manager2 = new MockSettingsManager();

            // Change stuff in manager2
            manager2.Class = new MockClass();
            manager2.Class.Decimal = 123123;

            // Copy
            manager1.CopyFrom(manager2);

            // Check values
            Assert.NotNull(manager1.Class);
            Assert.Equal(123123, manager1.Class!.Decimal);
        }

        [Fact]
        public void CloneTest()
        {
            var manager = new MockSettingsManager();

            // Change stuff
            manager.Array = new ushort[] {99, 5, 6};

            // Clone
            var clone = manager.Clone() as MockSettingsManager;
            Assert.NotNull(clone);

            // Check values
            Assert.NotNull(clone!.Array);
            Assert.Equal(3, clone.Array.Length);
            Assert.Equal(99, clone.Array[0]);
            Assert.Equal(5, clone.Array[1]);
            Assert.Equal(6, clone.Array[2]);
        }

        [Fact]
        public void PropertyChangedTest()
        {
            var manager = new MockSettingsManager();
            string? changedProperty = null;
            manager.PropertyChanged += (sender, args) =>
            {
                // Ignore IsSaved changing
                if (args.PropertyName == nameof(manager.IsSaved))
                    return;

                // Save the name of the changed property
                changedProperty = args.PropertyName;
            };

            // Change value
            manager.DateTime = DateTime.UtcNow;

            // Check if event raised correctly
            Assert.NotNull(changedProperty);
            Assert.Equal(nameof(manager.DateTime), changedProperty);
        }

        [Fact]
        public void PropertyChangedDistinctTest()
        {
            var manager = new MockSettingsManager();
            var triggerCount = 0;
            manager.PropertyChanged += (sender, args) =>
            {
                // Ignore IsSaved changing
                if (args.PropertyName == nameof(manager.IsSaved))
                    return;

                // Count how many times event was raised
                triggerCount++;
            };

            // Change value
            manager.Enum = MockEnum.Three;

            // Change value again, to the same thing
            manager.Enum = MockEnum.Three;
            manager.Enum = MockEnum.Three;

            // Change value to a new thing
            manager.Enum = MockEnum.One;

            // Change value again, to the same thing
            manager.Enum = MockEnum.One;
            manager.Enum = MockEnum.One;

            // Check if event was only raised the minimum number of times
            Assert.Equal(2, triggerCount);
        }

        [Fact]
        public void SaveTest()
        {
            var manager = new MockSettingsManager();

            // Save
            manager.Save();
            Assert.True(File.Exists(manager.FullFilePath));
        }

        [Fact]
        public void SaveChangesTest()
        {
            var manager = new MockSettingsManager();

            // Change some values
            manager.Int = 13;
            manager.Double = 3.14;

            // Save
            manager.Save();

            // Re-create
            manager = new MockSettingsManager();

            // Load
            manager.Load();

            // Check values
            Assert.Equal(13, manager.Int);
            Assert.Equal(3.14, manager.Double);
            Assert.Equal("Hello World", manager.Str);
        }

        [Fact]
        public void IsSavedTest()
        {
            var manager = new MockSettingsManager();

            // IsSaved should be true because it's persistent (all values are default)
            Assert.True(manager.IsSaved);

            // Change values
            manager.DateTime = DateTime.Now;

            // IsSaved should be false because a value was changed
            Assert.False(manager.IsSaved);

            // Save
            manager.Save();

            // IsSaved should be true because the settings are saved
            Assert.True(manager.IsSaved);

            // Change values
            manager.Int = 43;

            // IsSaved should be false because a value was changed
            Assert.False(manager.IsSaved);

            // Load
            manager.Load();

            // IsSaved should be true because the settings are saved
            Assert.True(manager.IsSaved);

            // Change values
            manager.Class = new MockClass();

            // IsSaved should be false because a value was changed
            Assert.False(manager.IsSaved);

            // Reset
            manager.Reset();

            // IsSaved should be false because it's not persistent anymore
            Assert.False(manager.IsSaved);
        }

        [Fact]
        public void ResetTest()
        {
            var manager = new MockSettingsManager();

            // Change some values
            manager.Str = "Test";
            manager.Enum = MockEnum.One;

            // Reset
            manager.Reset();

            // Check values
            Assert.Equal("Hello World", manager.Str);
            Assert.Equal(MockEnum.Two, manager.Enum);
        }

        [Fact]
        public void DeleteTest()
        {
            var manager = new MockSettingsManager();

            // Save
            manager.Save();

            // Delete
            manager.Delete();
            Assert.False(File.Exists(manager.FullFilePath));
        }

        [Fact]
        public void DeleteWithDirTest()
        {
            var manager = new MockSettingsManager();

            // Save
            manager.Save();

            // Delete
            manager.Delete(true);
            Assert.False(File.Exists(manager.FullFilePath));
            Assert.False(Directory.Exists(manager.FullDirectoryPath));
        }

        [Fact]
        public void SaveUpgradeLoadTest()
        {
            var oldManager = new MockSettingsManager();

            // Set some values
            oldManager.Double = 66.55;
            oldManager.Class = new MockClass();
            oldManager.Class.Long = 132;

            // Save
            oldManager.Save();

            // Upgrade
            var newManager = new MockSettingsManagerNewer();

            // Load
            newManager.Load();

            // Check values
            Assert.Equal(66.55, newManager.Double);
            Assert.NotNull(newManager.Class);
            Assert.Equal(132, newManager.Class!.Long);
            Assert.Equal('Q', newManager.Char);
        }

        public void Dispose()
        {
            var mock = new MockSettingsManager();
            try
            {
                Directory.Delete(mock.FullDirectoryPath, true);
            }
            catch (DirectoryNotFoundException)
            {
            }
        }
    }
}
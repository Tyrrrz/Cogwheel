using Cogwheel.Tests.Mocks;
using Xunit;

namespace Cogwheel.Tests
{
    public class StagerTests
    {
        [Fact]
        public void InstantiateTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Make sure both instances exist and are not the same
            Assert.NotNull(stager.Stable);
            Assert.NotNull(stager.Dirty);
            Assert.False(ReferenceEquals(stager.Stable, stager.Dirty));
        }

        [Fact]
        public void InstantiateWithFactoryTest()
        {
            var stager = new Stager<MockSettingsManager>(
                new MockSettingsManager {Int = 1337},
                new MockSettingsManager {Int = 6969});

            // Make sure both instances exist and are not the same
            Assert.NotNull(stager.Stable);
            Assert.NotNull(stager.Dirty);
            Assert.False(ReferenceEquals(stager.Stable, stager.Dirty));

            // Check if factory worked
            Assert.Equal(1337, stager.Stable.Int);
            Assert.Equal(6969, stager.Dirty.Int);
        }

        [Fact]
        public void SaveSyncTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Should be in sync
            Assert.Equal(stager.Dirty.Str, stager.Stable.Str);

            // Change value
            stager.Dirty.Str = "553322";

            // Should not be in sync
            Assert.NotEqual(stager.Dirty.Str, stager.Stable.Str);

            // Save
            stager.Save();

            // Should be in sync
            Assert.Equal(stager.Dirty.Str, stager.Stable.Str);
        }

        [Fact]
        public void LoadSyncTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Save
            stager.Save();

            // Should be in sync
            Assert.Equal(stager.Dirty.Str, stager.Stable.Str);

            // Change value
            stager.Dirty.Str = "553322";

            // Should not be in sync
            Assert.NotEqual(stager.Dirty.Str, stager.Stable.Str);

            // Load
            stager.Load();

            // Should be in sync
            Assert.Equal(stager.Dirty.Str, stager.Stable.Str);
        }

        [Fact]
        public void RevertStagingSyncTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Should be in sync
            Assert.Equal(stager.Dirty.Str, stager.Stable.Str);

            // Change value
            stager.Dirty.Str = "553322";

            // Should not be in sync
            Assert.NotEqual(stager.Dirty.Str, stager.Stable.Str);

            // Save
            stager.Save();

            // Should be in sync
            Assert.Equal(stager.Dirty.Str, stager.Stable.Str);
        }
    }
}
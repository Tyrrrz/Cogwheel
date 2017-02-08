using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Settings.Tests.Mocks;
using Tyrrrz.Settings;

namespace Settings.Tests
{
    [TestClass]
    public class StagerTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            var manager = new TestSettingsManager();

            // Try to delete everything
            try
            {
                Directory.Delete(manager.Configuration.FullDirectoryPath, true);
            }
            catch (DirectoryNotFoundException)
            {
            }
        }

        [TestMethod]
        public void InstantiateTest()
        {
            var stager = new Stager<TestSettingsManager>();

            // Make sure both instances exist and are not the same
            Assert.IsNotNull(stager.Current);
            Assert.IsNotNull(stager.Staging);
            Assert.IsFalse(ReferenceEquals(stager.Current, stager.Staging));
        }

        [TestMethod]
        public void InstantiateWithFactoryTest()
        {
            var stager = new Stager<TestSettingsManager>(() => new TestSettingsManager {Int = 1337});

            // Make sure both instances exist and are not the same
            Assert.IsNotNull(stager.Current);
            Assert.IsNotNull(stager.Staging);
            Assert.IsFalse(ReferenceEquals(stager.Current, stager.Staging));

            // Check if factory worked
            Assert.AreEqual(1337, stager.Current.Int);
            Assert.AreEqual(1337, stager.Staging.Int);
        }

        [TestMethod]
        public void SaveSyncTest()
        {
            var stager = new Stager<TestSettingsManager>();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);

            // Change value
            stager.Staging.Str = "553322";

            // Should not be in sync
            Assert.AreNotEqual(stager.Staging.Str, stager.Current.Str);

            // Save
            stager.Save();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);
        }

        [TestMethod]
        public void LoadSyncTest()
        {
            var stager = new Stager<TestSettingsManager>();

            // Save
            stager.Save();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);

            // Change value
            stager.Staging.Str = "553322";

            // Should not be in sync
            Assert.AreNotEqual(stager.Staging.Str, stager.Current.Str);

            // Load
            stager.Load();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);
        }

        [TestMethod]
        public void RevertStagingSyncTest()
        {
            var stager = new Stager<TestSettingsManager>();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);

            // Change value
            stager.Staging.Str = "553322";

            // Should not be in sync
            Assert.AreNotEqual(stager.Staging.Str, stager.Current.Str);

            // Save
            stager.Save();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);
        }
    }
}

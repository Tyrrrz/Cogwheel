using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrrrz.Settings.Tests.Mocks;

namespace Tyrrrz.Settings.Tests
{
    [TestClass]
    public class SettingsManagerTests
    {
        [TestMethod]
        public void InstantiateTest()
        {
            var manager = new FakeSettingsManager();

            // Check that configuration properties are set
            Assert.IsNotNull(manager.Configuration);
            Assert.IsNotNull(manager.Configuration.SubDirectoryPath);
            Assert.IsNotNull(manager.Configuration.FileName);
            Assert.IsNotNull(manager.Configuration.FullDirectoryPath);
            Assert.IsNotNull(manager.Configuration.FullFilePath);
        }

        [TestMethod]
        public void CopyFromTest()
        {
            var manager1 = new FakeSettingsManager();
            var manager2 = new FakeSettingsManager();

            // Change stuff in manager2
            manager2.Class = new FakeClass();
            manager2.Class.Decimal = 123123;

            // Copy
            manager1.CopyFrom(manager2);

            // Check values
            Assert.IsNotNull(manager1.Class);
            Assert.AreEqual(123123, manager1.Class.Decimal);
        }

        [TestMethod]
        public void CloneTest()
        {
            var manager = new FakeSettingsManager();

            // Change stuff
            manager.Array = new ushort[] {99, 5, 6};

            // Clone
            var clone = manager.Clone() as FakeSettingsManager;
            Assert.IsNotNull(clone);

            // Check values
            Assert.IsNotNull(clone.Array);
            Assert.AreEqual(3, clone.Array.Length);
            Assert.AreEqual(99, clone.Array[0]);
            Assert.AreEqual(5, clone.Array[1]);
            Assert.AreEqual(6, clone.Array[2]);
        }

        [TestMethod]
        public void PropertyChangedTest()
        {
            var manager = new FakeSettingsManager();
            string changedProperty = null;
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
            Assert.IsNotNull(changedProperty);
            Assert.AreEqual(nameof(manager.DateTime), changedProperty);
        }

        [TestMethod]
        public void PropertyChangedDistinctTest()
        {
            var manager = new FakeSettingsManager();
            int triggerCount = 0;
            manager.PropertyChanged += (sender, args) =>
            {
                // Ignore IsSaved changing
                if (args.PropertyName == nameof(manager.IsSaved))
                    return;

                // Count how many times event was raised
                triggerCount++;
            };

            // Change value
            manager.Enum = FakeEnum.Three;

            // Change value again, to the same thing
            manager.Enum = FakeEnum.Three;
            manager.Enum = FakeEnum.Three;

            // Change value to a new thing
            manager.Enum = FakeEnum.One;

            // Change value again, to the same thing
            manager.Enum = FakeEnum.One;
            manager.Enum = FakeEnum.One;

            // Check if event was only raised the minimum number of times
            Assert.AreEqual(2, triggerCount);
        }

        [TestMethod]
        public void SaveTest()
        {
            var manager = new FakeSettingsManager();

            // Save
            manager.Save();
            Assert.IsTrue(FakeFileSystemService.Instance.FileExists(manager.Configuration.FullFilePath));
        }

        [TestMethod]
        public void SaveChangesTest()
        {
            var manager = new FakeSettingsManager();

            // Change some values
            manager.Int = 13;
            manager.Double = 3.14;

            // Save
            manager.Save();

            // Re-create
            manager = new FakeSettingsManager();

            // Load
            manager.Load();

            // Check values
            Assert.AreEqual(13, manager.Int);
            Assert.AreEqual(3.14, manager.Double);
            Assert.AreEqual("Hello World", manager.Str);
        }

        [TestMethod]
        public void IsSavedTest()
        {
            var manager = new FakeSettingsManager();

            // IsSaved should be true because it's persistent (all values are default)
            Assert.IsTrue(manager.IsSaved);

            // Change values
            manager.DateTime = DateTime.Now;

            // IsSaved should be false because a value was changed
            Assert.IsFalse(manager.IsSaved);

            // Save
            manager.Save();

            // IsSaved should be true because the settings are saved
            Assert.IsTrue(manager.IsSaved);

            // Change values
            manager.Int = 43;

            // IsSaved should be false because a value was changed
            Assert.IsFalse(manager.IsSaved);

            // Load
            manager.Load();

            // IsSaved should be true because the settings are saved
            Assert.IsTrue(manager.IsSaved);

            // Change values
            manager.Class = new FakeClass();

            // IsSaved should be false because a value was changed
            Assert.IsFalse(manager.IsSaved);

            // Reset
            manager.Reset();

            // IsSaved should be false because it's not persistent anymore
            Assert.IsFalse(manager.IsSaved);
        }

        [TestMethod]
        public void ResetTest()
        {
            var manager = new FakeSettingsManager();

            // Change some values
            manager.Str = "Test";
            manager.Enum = FakeEnum.One;

            // Reset
            manager.Reset();

            // Check values
            Assert.AreEqual("Hello World", manager.Str);
            Assert.AreEqual(FakeEnum.Two, manager.Enum);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var manager = new FakeSettingsManager();

            // Save
            manager.Save();

            // Delete
            manager.Delete();
            Assert.IsFalse(FakeFileSystemService.Instance.FileExists(manager.Configuration.FullFilePath));
        }

        [TestMethod]
        public void DeleteWithDirTest()
        {
            var manager = new FakeSettingsManager();

            // Save
            manager.Save();

            // Delete
            manager.Delete(true);
            Assert.IsFalse(FakeFileSystemService.Instance.FileExists(manager.Configuration.FullFilePath));
            Assert.IsFalse(FakeFileSystemService.Instance.DirectoryExists(manager.Configuration.FullDirectoryPath));
        }

        [TestMethod]
        public void SaveUpgradeLoadTest()
        {
            var oldManager = new FakeSettingsManager();

            // Set some values
            oldManager.Double = 66.55;
            oldManager.Class = new FakeClass();
            oldManager.Class.Long = 132;

            // Save
            oldManager.Save();

            // Upgrade
            var newManager = new FakeSettingsManagerNewer();

            // Load
            newManager.Load();
            
            // Check values
            Assert.AreEqual(66.55, newManager.Double);
            Assert.IsNotNull(newManager.Class);
            Assert.AreEqual(132, newManager.Class.Long);
            Assert.AreEqual('Q', newManager.Char);
        }
    }
}

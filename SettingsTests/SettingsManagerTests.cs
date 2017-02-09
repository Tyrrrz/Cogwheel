using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Settings.Tests.Mocks;

namespace Settings.Tests
{
    [TestClass]
    public class SettingsManagerTests
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
            var manager = new TestSettingsManager();

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
            var manager1 = new TestSettingsManager();
            var manager2 = new TestSettingsManager();

            // Change stuff in manager2
            manager2.Class = new TestClass();
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
            var manager = new TestSettingsManager();

            // Change stuff
            manager.Array = new ushort[] {99, 5, 6};

            // Clone
            var clone = manager.Clone() as TestSettingsManager;
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
            var manager = new TestSettingsManager();
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
            var manager = new TestSettingsManager();
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
            manager.Enum = TestEnum.Three;

            // Change value again, to the same thing
            manager.Enum = TestEnum.Three;
            manager.Enum = TestEnum.Three;

            // Change value to a new thing
            manager.Enum = TestEnum.One;

            // Change value again, to the same thing
            manager.Enum = TestEnum.One;
            manager.Enum = TestEnum.One;

            // Check if event was only raised the minimum number of times
            Assert.AreEqual(2, triggerCount);
        }

        [TestMethod]
        public void SaveTest()
        {
            var manager = new TestSettingsManager();

            // Save
            manager.Save();
            Assert.IsTrue(File.Exists(manager.Configuration.FullFilePath));
        }

        [TestMethod]
        public void SaveChangesTest()
        {
            var manager = new TestSettingsManager();

            // Change some values
            manager.Int = 13;
            manager.Double = 3.14;

            // Save
            manager.Save();

            // Re-create
            manager = new TestSettingsManager();

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
            var manager = new TestSettingsManager();

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
            manager.Class = new TestClass();

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
            var manager = new TestSettingsManager();

            // Change some values
            manager.Str = "Test";
            manager.Enum = TestEnum.One;

            // Reset
            manager.Reset();

            // Check values
            Assert.AreEqual("Hello World", manager.Str);
            Assert.AreEqual(TestEnum.Two, manager.Enum);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var manager = new TestSettingsManager();

            // Save
            manager.Save();

            // Delete
            manager.Delete();
            Assert.IsFalse(File.Exists(manager.Configuration.FullFilePath));
        }

        [TestMethod]
        public void DeleteWithDirTest()
        {
            var manager = new TestSettingsManager();

            // Save
            manager.Save();

            // Delete
            manager.Delete(true);
            Assert.IsFalse(File.Exists(manager.Configuration.FullFilePath));
            Assert.IsFalse(Directory.Exists(manager.Configuration.FullDirectoryPath));
        }

        [TestMethod]
        public void SaveUpgradeLoadTest()
        {
            var oldManager = new TestSettingsManager();

            // Set some values
            oldManager.Double = 66.55;
            oldManager.Class = new TestClass();
            oldManager.Class.Long = 132;

            // Save
            oldManager.Save();

            // Upgrade
            var newManager = new TestSettingsManagerNewer();

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

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
        public void SaveChangeLoadTest()
        {
            var manager = new TestSettingsManager();

            // Change some values
            manager.Int = 13;
            manager.Double = 3.14;

            // Save
            manager.Save();
            Assert.IsTrue(File.Exists(manager.Configuration.FullFilePath));

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
        public void ChangeResetTest()
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
            Assert.IsTrue(File.Exists(manager.Configuration.FullFilePath));

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
            Assert.IsTrue(File.Exists(manager.Configuration.FullFilePath));

            // Delete
            manager.Delete(true);
            Assert.IsFalse(File.Exists(manager.Configuration.FullFilePath));
            Assert.IsFalse(Directory.Exists(manager.Configuration.FullDirectoryPath));
        }
    }
}

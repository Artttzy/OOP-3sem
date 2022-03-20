using Backups.Objects;
using Backups.Tests.Storage;
using NUnit.Framework;
using System;
using System.Linq;

namespace Backups.Tests
{
    [TestFixture]
    public class Test
    {
        private BackupJob job;

        [SetUp]
        public void SetUp()
        {
            job = new BackupJob("test ram job", new RamStorageFactory());
        }

        [Test]
        public void CreateBackupInRamStorage_FilesAndRestorePointExist()
        {
            Assert.DoesNotThrow(() =>
            {
                job.AddFile(new FileData
                {
                    FileName = "a.txt",
                    Content = RandomBytes(512)
                });

                job.AddFile(new FileData
                {
                    FileName = "b.txt",
                    Content = RandomBytes(1024)
                });

                job.CreateRestorePoint();

                job.RemoveFile(new FileData
                {
                    FileName = "a.txt"
                });

                job.CreateRestorePoint();
            });
            var restorePoints = job.GetRestorePoints().ToList();
            Assert.AreEqual(restorePoints.Count, 2);
            
            Assert.AreEqual(restorePoints[0].ReadFiles().Count(), 2);
            Assert.AreEqual(restorePoints[1].ReadFiles().Count(), 1);

            Assert.AreEqual(restorePoints[1].ReadFiles().First().FileName, "b.txt");
            Assert.AreEqual(restorePoints[1].Version, 2);
        }

        private byte[] RandomBytes(int count)
        {
            Random random = new();
            byte[] randomBytes = new byte[count];
            random.NextBytes(randomBytes);
            return randomBytes;
        }
    }
}

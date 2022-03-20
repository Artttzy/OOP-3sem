using Backups;
using Backups.Objects;
using Backups.Tools;
using BackupsExtra.Interfaces;
using BackupsExtra.Services;
using BackupsExtra.Tests.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class RestoreCleanTest
    {
        private IExtendedRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new InMemoryRepositoryService();
        }

        [Test]
        public void Test()
        {
            var job = new BackupJob("Clean test job", new SplitStorageFactory
            {
                Repository = repository
            });

            for (int i = 0; i < 5; i++)
            {
                job.AddFile(new FileData
                {
                    FileName = $"File{i + 1}.txt",
                    Content = Encoding.Default.GetBytes($"Test file {i + 1}")
                });

                job.CreateRestorePoint();
            }

            IRestorePointFilter filter = new CountRestorePointFilter(
                new SplitStorageAdapter(repository),
                job.GetRestorePoints(),
                3);

            IEnumerable<RestorePoint> deleted = filter.GetDeletedRestorePoints();
            Assert.AreEqual(deleted.Count(), 2);
        }
    }
}

using Backups;
using Backups.Tools;
using BackupsExtra.Helpers;
using BackupsExtra.Interfaces;
using BackupsExtra.State;
using BackupsExtra.Tests.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class TestState
    {
        private IExtendedRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new InMemoryRepositoryService();
        }

        [Test]
        public void TestCreateConfig()
        {
            Assert.DoesNotThrow(() =>
            {
                var job = new BackupJob("Test job", new SplitStorageFactory()
                {
                    Repository = repository
                });

                BackupJobState state = JobToStateConverter.Pack(job);
                var stateRepository = new BackupJobStateRepository(repository);
                stateRepository.Write(state);
            });
        }

        [Test]
        public void TestReadConfig()
        {
            Assert.DoesNotThrow(() =>
            {
                var stateRepository = new BackupJobStateRepository(repository);
                BackupJobState state = stateRepository.Read();
                BackupJob backupJob = JobToStateConverter.Unpack(state);
                Assert.AreEqual("Test job", backupJob.Name);
            });
        }
    }
}

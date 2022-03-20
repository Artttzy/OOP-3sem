using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Backups;
using Backups.Interfaces;
using Backups.Tools;
using BackupsExtra.Exceptions;
using BackupsExtra.State;

namespace BackupsExtra.Helpers
{
    public static class JobToStateConverter
    {
        public static BackupJobState Pack(BackupJob backupJob)
        {
            IEnumerable<RestorePoint> restorePoints = backupJob.GetRestorePoints();

            int version = (int)backupJob
                .GetType()
                .GetField("version", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(backupJob);

            var state = new BackupJobState()
            {
                Version = version,
                Name = backupJob.Name,
                RestorePoints = restorePoints.Select(
                    rp => StorageModeToStateConverter.Pack(GetStorageMode(rp))).ToList(),
            };

            state.StorageType = state.RestorePoints.FirstOrDefault()?.StorageType ?? StorageType.SingleStorage;
            state.Repository = state.RestorePoints.FirstOrDefault()?.Repository;

            return state;
        }

        public static BackupJob Unpack(BackupJobState jobState)
        {
            IStorageFactory storageFactory = jobState.StorageType switch
            {
                StorageType.SingleStorage => new SingleStorageFactory()
                {
                    Repository = RepositoryToStateConverter.Unpack(jobState.Repository),
                },

                StorageType.SplitStorage => new SplitStorageFactory()
                {
                    Repository = RepositoryToStateConverter.Unpack(jobState.Repository),
                },

                _ => throw new UnexpectedStateException("Storage should be single or split"),
            };

            return new BackupJob(jobState.Name, storageFactory);
        }

        private static IStorageMode GetStorageMode(RestorePoint restorePoint)
        {
           return restorePoint.GetType().GetField("storage", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(restorePoint) as IStorageMode;
        }
    }
}

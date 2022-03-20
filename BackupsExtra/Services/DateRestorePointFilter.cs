using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Exceptions;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class DateRestorePointFilter : IRestorePointFilter
    {
        public DateRestorePointFilter(IStorageAdapter storageAdapter, IEnumerable<RestorePoint> restorePoints, DateTime deleteBefore)
        {
            StorageAdapter = storageAdapter;
            RestorePoints = restorePoints;
            DeleteBefore = deleteBefore.ToUniversalTime();
        }

        public IStorageAdapter StorageAdapter { get; }
        public IEnumerable<RestorePoint> RestorePoints { get; }
        public DateTime DeleteBefore { get; }

        public IEnumerable<RestorePoint> GetDeletedRestorePoints()
        {
            IEnumerable<RestorePoint> deleted = RestorePoints.Where(r => StorageAdapter.GetCreationTime(r) < DeleteBefore);

            // Удалены все точки.
            if (deleted.Count() == RestorePoints.Count())
            {
                throw new AllRestorePointsDeletedException();
            }

            return deleted;
        }
    }
}

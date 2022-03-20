using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Exceptions;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class CountRestorePointFilter : IRestorePointFilter
    {
        public CountRestorePointFilter(IStorageAdapter storageAdapter, IEnumerable<RestorePoint> restorePoints, int leaveLast = 3)
        {
            StorageAdapter = storageAdapter;
            RestorePoints = restorePoints;
            LeaveLast = leaveLast;
        }

        public IStorageAdapter StorageAdapter { get; }
        public IEnumerable<RestorePoint> RestorePoints { get; }
        public int LeaveLast { get; }

        public IEnumerable<RestorePoint> GetDeletedRestorePoints()
        {
            IEnumerable<RestorePoint> deleted = RestorePoints.OrderByDescending(r => StorageAdapter.GetIndex(r)).Skip(LeaveLast);

            // Удалены все точки.
            if (deleted.Count() == RestorePoints.Count())
            {
                throw new AllRestorePointsDeletedException();
            }

            return deleted;
        }
    }
}

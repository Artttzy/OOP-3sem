using System.Collections.Generic;
using Backups;

namespace BackupsExtra.Interfaces
{
    public interface IRestorePointFilter
    {
        IStorageAdapter StorageAdapter { get; }
        IEnumerable<RestorePoint> GetDeletedRestorePoints();
    }
}

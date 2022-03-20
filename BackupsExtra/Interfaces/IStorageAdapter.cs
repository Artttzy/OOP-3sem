using System;
using Backups;

namespace BackupsExtra.Interfaces
{
    public interface IStorageAdapter
    {
        public DateTime GetCreationTime(RestorePoint restorePoint);
        public int GetIndex(RestorePoint restorePoint);
    }
}

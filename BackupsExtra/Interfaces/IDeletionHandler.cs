using Backups;

namespace BackupsExtra.Interfaces
{
    public interface IDeletionHandler
    {
        void DeleteRestorePoint(RestorePoint restorePoint);
    }
}

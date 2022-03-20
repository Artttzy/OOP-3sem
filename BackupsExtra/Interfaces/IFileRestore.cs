using Backups.Objects;

namespace BackupsExtra.Interfaces
{
    public interface IFileRestore
    {
        void RestoreFile(FileData fileData);
    }
}

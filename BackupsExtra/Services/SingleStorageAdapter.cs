using System;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class SingleStorageAdapter : IStorageAdapter
    {
        public SingleStorageAdapter(IExtendedRepository storageRepository)
        {
            StorageRepository = storageRepository;
        }

        public IExtendedRepository StorageRepository { get; }

        public DateTime GetCreationTime(RestorePoint restorePoint)
        {
            string path = $"backup_{restorePoint.Version}.zip";
            return StorageRepository.GetFileCreationTime(path);
        }

        public int GetIndex(RestorePoint restorePoint)
        {
            return restorePoint.Version;
        }
    }
}

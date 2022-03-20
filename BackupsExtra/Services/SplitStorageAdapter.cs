using System;
using System.Linq;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class SplitStorageAdapter : IStorageAdapter
    {
        public SplitStorageAdapter(IExtendedRepository storageRepository)
        {
            StorageRepository = storageRepository;
        }

        public IExtendedRepository StorageRepository { get; }

        public DateTime GetCreationTime(RestorePoint restorePoint)
        {
            string entry = restorePoint.ReadFiles().First().FileName + $"_{restorePoint.Version}.zip";
            return StorageRepository.GetFileCreationTime(entry);
        }

        public int GetIndex(RestorePoint restorePoint)
        {
            return restorePoint.Version;
        }
    }
}

using System.Reflection;
using Backups;
using Backups.Interfaces;
using BackupsExtra.Exceptions;
using BackupsExtra.State;

namespace BackupsExtra.Helpers
{
    public static class StorageModeToStateConverter
    {
        public static StorageState Pack(IStorageMode storageMode)
        {
            StorageType storageType = storageMode switch
            {
                SingleStorage _ => StorageType.SingleStorage,
                SplitStorage _ => StorageType.SplitStorage,
                _ => throw new UnexpectedStateException("Storage should be single or split"),
            };

            int version = (int)storageMode.GetType().GetField("_version", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(storageMode);

            var repository = storageType.GetType().GetField("_repository", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(storageMode) as IRepository;
            RepositoryState repositoryState = RepositoryToStateConverter.Pack(repository);

            return new StorageState()
            {
                Repository = repositoryState,
                Version = version,
                StorageType = storageType,
            };
        }

        public static IStorageMode Unpack(StorageState storageState)
        {
            return storageState.StorageType switch
            {
                StorageType.SingleStorage => new SingleStorage(RepositoryToStateConverter.Unpack(storageState.Repository), storageState.Version),
                StorageType.SplitStorage => new SplitStorage(RepositoryToStateConverter.Unpack(storageState.Repository), storageState.Version),
                _ => throw new UnexpectedStateException("Storage type не single и не split")
            };
        }
    }
}

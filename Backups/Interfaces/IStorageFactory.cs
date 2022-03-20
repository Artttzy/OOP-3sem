namespace Backups.Interfaces
{
    public interface IStorageFactory
    {
        IStorageMode GetStorage(int version);
        int GetVersion();
    }
}

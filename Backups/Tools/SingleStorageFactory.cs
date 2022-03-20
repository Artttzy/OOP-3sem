using Backups.Interfaces;

namespace Backups.Tools
{
    public class SingleStorageFactory : IStorageFactory
    {
        public IRepository Repository { get; set; }

        public IStorageMode GetStorage(int version)
        {
            return new SingleStorage(Repository, version);
        }

        public int GetVersion()
        {
            int version = 0;
            while (Repository.FileExists($"backup_{version + 1}.zip") /*File.Exists(Path + $"/backup_{version + 1}.zip")*/)
            {
                version++;
            }

            return version;
        }
    }
}

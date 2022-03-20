using System.Collections.Generic;
using System.Linq;
using Backups.Objects;

namespace Backups
{
    public class RestorePoint
    {
        private readonly IStorageMode storage;

        public RestorePoint(IStorageMode storage, int version)
        {
            this.storage = storage;
            Version = version;
        }

        public int Version { get; }

        public List<FileData> ReadFiles()
        {
            return storage.ReadFiles().ToList();
        }
    }
}

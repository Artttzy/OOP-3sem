using Backups.Objects;
using System.Collections.Generic;

namespace Backups.Tests.Storage
{
    internal class RamStorage : IStorageMode
    {
        private readonly List<FileData> files;

        public RamStorage(List<FileData> files)
        {
            this.files = files;
        }

        public void AddFile(FileData data)
        {
            files.Add(data);
        }

        public IEnumerable<FileData> ReadFiles()
        {
            return files;
        }
    }
}

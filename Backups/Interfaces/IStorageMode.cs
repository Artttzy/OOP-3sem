using System.Collections.Generic;
using Backups.Objects;

namespace Backups
{
    public interface IStorageMode
    {
        void AddFile(FileData data);

        IEnumerable<FileData> ReadFiles();
    }
}

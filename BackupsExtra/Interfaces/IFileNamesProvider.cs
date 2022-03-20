using System.Collections.Generic;
using Backups;

namespace BackupsExtra.Interfaces
{
    public interface IFileNamesProvider
    {
        IEnumerable<string> FileNames(RestorePoint restorePoint);
    }
}

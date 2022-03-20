using System.Collections.Generic;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class SingleFileNamesProvider : IFileNamesProvider
    {
        public IEnumerable<string> FileNames(RestorePoint restorePoint)
        {
            return new string[] { $"backup_${restorePoint.Version}.zip" };
        }
    }
}

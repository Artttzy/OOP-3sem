using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class SplitFileNamesProvider : IFileNamesProvider
    {
        public IEnumerable<string> FileNames(RestorePoint restorePoint)
        {
            return restorePoint.ReadFiles().Select(fileInfo => EntryNameToFileName(restorePoint.Version, fileInfo.FileName));
        }

        private string EntryNameToFileName(int version, string entryName)
        {
            return entryName + $"_{version}.zip";
        }
    }
}

using System.Collections.Generic;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class RemoveDeletionHandler : IDeletionHandler
    {
        public RemoveDeletionHandler(IExtendedRepository repository, IFileNamesProvider fileNamesProvider)
        {
            Repository = repository;
            FileNamesProvider = fileNamesProvider;
        }

        public IExtendedRepository Repository { get; }
        public IFileNamesProvider FileNamesProvider { get; }

        public void DeleteRestorePoint(RestorePoint restorePoint)
        {
            IEnumerable<string> filesToDelete = FileNamesProvider.FileNames(restorePoint);

            foreach (string file in filesToDelete)
            {
                Repository.DeleteFile(file);
            }
        }
    }
}

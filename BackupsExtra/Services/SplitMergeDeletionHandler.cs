using System.Collections.Generic;
using System.Linq;
using Backups;
using Backups.Objects;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class SplitMergeDeletionHandler : IMergeDeletionHandler
    {
        public SplitMergeDeletionHandler(RestorePoint mergeTo, IExtendedRepository repositoryService)
        {
            MergeTo = mergeTo;
            RepositoryService = repositoryService;
        }

        public RestorePoint MergeTo { get; }
        public IExtendedRepository RepositoryService { get; }

        public void DeleteRestorePoint(RestorePoint restorePoint)
        {
            List<FileData> oldFiles = restorePoint.ReadFiles();
            List<FileData> newFiles = MergeTo.ReadFiles();

            foreach (FileData file in oldFiles)
            {
                HandleFile(file, newFiles, restorePoint.Version);
            }
        }

        private void HandleFile(FileData file, List<FileData> newFiles, int oldVersion)
        {
            string baseFileName = file.FileName;
            string oldFileName = $"{baseFileName}_{oldVersion}.zip";

            if (!newFiles.Any(f => f.FileName == baseFileName))
            {
                string newFileName = $"{baseFileName}_{MergeTo.Version}.zip";
                RepositoryService.MoveFile(oldFileName, newFileName);
            }

            RepositoryService.DeleteFile(oldFileName);
        }
    }
}

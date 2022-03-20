using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class SingleMergeDeletionHandler : IMergeDeletionHandler
    {
        public SingleMergeDeletionHandler(IExtendedRepository repositoryService)
        {
            RepositoryService = repositoryService;
        }

        public IExtendedRepository RepositoryService { get; }

        public void DeleteRestorePoint(RestorePoint restorePoint)
        {
            string fileName = $"backup_{restorePoint.Version}.zip";
            RepositoryService.DeleteFile(fileName);
        }
    }
}

using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class FileLogger : ILogger
    {
        public FileLogger(string fileName, IExtendedRepository repositoryService)
        {
            FileName = fileName;
            RepositoryService = repositoryService;
        }

        public string FileName { get; }
        public IExtendedRepository RepositoryService { get; }

        public void Log(string message)
        {
            RepositoryService.AppendLine(FileName, message);
        }
    }
}

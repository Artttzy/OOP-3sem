using System.IO;
using Backups.Objects;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class LocationFileRestore : IFileRestore
    {
        public LocationFileRestore(IExtendedRepository locationRepository)
        {
            LocationRepository = locationRepository;
        }

        public IExtendedRepository LocationRepository { get; }

        public void RestoreFile(FileData fileData)
        {
            if (LocationRepository.FileExists(fileData.FileName))
            {
                LocationRepository.DeleteFile(fileData.FileName);
            }

            using Stream repositoryStream = LocationRepository.GetRepositoryStream(fileData.FileName, false);
            repositoryStream.Write(fileData.Content);
        }
    }
}

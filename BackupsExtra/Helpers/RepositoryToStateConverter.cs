using System.Reflection;
using Backups.Interfaces;
using BackupsExtra.Services;
using BackupsExtra.State;

namespace BackupsExtra.Helpers
{
    public static class RepositoryToStateConverter
    {
        public static RepositoryState Pack(IRepository repository)
        {
            string path = (string)repository.GetType().GetField("path", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(repository);
            return new RepositoryState()
            {
                Path = path,
            };
        }

        public static IRepository Unpack(RepositoryState state)
        {
            return new FileRepositoryService(state?.Path);
        }
    }
}

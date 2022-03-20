using System.Collections.Generic;
using System.IO;

namespace Backups.Interfaces
{
    public interface IRepository
    {
        Stream GetRepositoryStream(string archiveName, bool read);
        bool FileExists(string archiveName);
        IEnumerable<string> EnumerateEntries(string pattern, bool recursive);
    }
}

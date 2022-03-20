using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups.Tools
{
    internal class FileRepository : IRepository
    {
        private readonly string path;

        public FileRepository(string path)
        {
            this.path = path;
        }

        public IEnumerable<string> EnumerateEntries(string pattern, bool recursive)
        {
            return Directory.EnumerateFiles(path, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        public bool FileExists(string archiveName)
        {
            return File.Exists(path + $"/{archiveName}");
        }

        public Stream GetRepositoryStream(string archiveName, bool read)
        {
            if (read)
            {
                return new FileStream(path + $"/{archiveName}", FileMode.Open, FileAccess.Read);
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return new FileStream(path + $"/{archiveName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
        }
    }
}

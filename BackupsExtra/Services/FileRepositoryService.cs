using System;
using System.Collections.Generic;
using System.IO;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class FileRepositoryService : IExtendedRepository
    {
        private readonly string path;

        public FileRepositoryService(string path)
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

        public void WriteToFile(string fileName, string content)
        {
            string file = Path.Combine(Path.GetFullPath(path), fileName);
            File.WriteAllText(file, content);
        }

        public string ReadFromFile(string fileName)
        {
            string file = Path.Combine(Path.GetFullPath(path), fileName);
            return File.ReadAllText(file);
        }

        public void AppendLine(string fileName, string line)
        {
            string file = Path.Combine(Path.GetFullPath(path), fileName);

            if (!File.Exists(file))
            {
                File.WriteAllText(file, string.Empty);
            }

            File.AppendAllLines(file, new string[] { line });
        }

        public DateTime GetFileCreationTime(string fileName)
        {
            string file = Path.Combine(Path.GetFullPath(path), fileName);
            return File.GetCreationTimeUtc(file);
        }

        public void DeleteFile(string fileName)
        {
            string file = Path.Combine(Path.GetFullPath(path), fileName);
            File.Delete(file);
        }

        public void MoveFile(string from, string toLocation)
        {
            string fileFrom = Path.Combine(Path.GetFullPath(path), from);
            string fileTo = Path.Combine(Path.GetFullPath(path), toLocation);

            File.Move(fileFrom, fileTo, false);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Objects;

namespace Backups
{
    public class SplitStorage : IStorageMode
    {
        private readonly int _version;
        private readonly IRepository _repository;

        public SplitStorage(IRepository repository, int version)
        {
            _repository = repository;
            _version = version;
        }

        public void AddFile(FileData data)
        {
            string fileName = data.FileName;

            using (var archive = new ZipArchive(_repository.GetRepositoryStream($"{fileName}_{_version}.zip", false), ZipArchiveMode.Create))
            {
                ZipArchiveEntry entry = archive.CreateEntry(data.FileName);
                using (Stream writer = entry.Open())
                {
                    writer.Write(data.Content, 0, data.Content.Length);
                }
            }

            using (var writer = new StreamWriter(_repository.GetRepositoryStream(".version", false)))
            {
                writer.Write(_version);
            }

            // File.WriteAllText(_pathToBackups + "/.version", _version.ToString());
        }

        public IEnumerable<FileData> ReadFiles()
        {
            foreach (string fileName in _repository.EnumerateEntries($"*_{_version}.zip", false))
            {
                using (var archive = new ZipArchive(_repository.GetRepositoryStream($"{fileName}", true), ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = archive.Entries[0];
                    using (Stream reader = entry.Open())
                    {
                        byte[] buffer = new byte[reader.Length];
                        reader.Read(buffer, 0, buffer.Length);

                        yield return new FileData
                        {
                            Content = buffer,
                            FileName = entry.Name,
                        };
                    }
                }
            }
        }
    }
}

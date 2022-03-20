using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Objects;

namespace Backups
{
    public class SingleStorage : IStorageMode
    {
        private readonly int _version;
        private readonly IRepository _repository;

        public SingleStorage(IRepository repository, int version)
        {
            _repository = repository;
            _version = version;
        }

        public void AddFile(FileData data)
        {
            // ZipArchiveMode zipMode = File.Exists(_repository + $"/backup_{_version}.zip") ? ZipArchiveMode.Update : ZipArchiveMode.Create;    new FileStream(_repository + $"/backup_{_version}.zip", FileMode.OpenOrCreate, FileAccess.ReadWrite)
            ZipArchiveMode zipMode = _repository.FileExists($"backup_{_version}.zip") ? ZipArchiveMode.Update : ZipArchiveMode.Create;
            using (var archive = new ZipArchive(_repository.GetRepositoryStream($"backup_{_version}.zip", false), zipMode))
            {
                ZipArchiveEntry entry = archive.CreateEntry(data.FileName);
                using (Stream writer = entry.Open())
                {
                    writer.Write(data.Content, 0, data.Content.Length);
                }
            }
        }

        public IEnumerable<FileData> ReadFiles()
        {
            using (var archive = new ZipArchive(_repository.GetRepositoryStream($"/backup_{_version}.zip", true), ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
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

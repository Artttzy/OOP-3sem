using System.Collections.Generic;
using Backups.Interfaces;
using Backups.Objects;

namespace Backups
{
    public class BackupJob
    {
        private readonly IStorageFactory _storageFactory;
        private int version = 1;
        private List<FileData> _files = new List<FileData>();
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();

        public BackupJob(string name, IStorageFactory storageFactory)
        {
            Name = name;
            _storageFactory = storageFactory;
        }

        public string Name { get; }

        public void AddFile(FileData file)
        {
            _files.Add(file);
        }

        public void RemoveFile(FileData file)
        {
            _files.RemoveAll(c => file.FileName == c.FileName);
        }

        public void CreateRestorePoint()
        {
            IStorageMode storageMode = _storageFactory.GetStorage(version);

            foreach (FileData data in _files)
            {
                storageMode.AddFile(data);
            }

            _restorePoints.Add(new RestorePoint(storageMode, version));
            version++;
        }

        public IEnumerable<RestorePoint> GetRestorePoints()
        {
            return _restorePoints;
        }
    }
}

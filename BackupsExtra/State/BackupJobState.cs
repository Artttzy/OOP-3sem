using System.Collections.Generic;

namespace BackupsExtra.State
{
    public class BackupJobState
    {
        public int Version { get; set; }

        public string Name { get; set; }

        public RepositoryState Repository { get; set; }

        public StorageType StorageType { get; set; }

        public ICollection<StorageState> RestorePoints { get; set; }
    }
}

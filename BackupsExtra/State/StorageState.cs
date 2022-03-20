namespace BackupsExtra.State
{
    public enum StorageType
    {
        /// <summary>
        /// Single Storage
        /// </summary>
        SingleStorage,

        /// <summary>
        /// Split Storage
        /// </summary>
        SplitStorage,
    }

    public class StorageState
    {
        public int Version { get; set; }

        public StorageType StorageType { get; set; }

        public RepositoryState Repository { get; set; }
    }
}

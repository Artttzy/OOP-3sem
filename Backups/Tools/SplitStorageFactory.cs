using System;
using System.IO;
using Backups.Interfaces;

namespace Backups.Tools
{
    public class SplitStorageFactory : IStorageFactory
    {
        public IRepository Repository { get; set; }

        public IStorageMode GetStorage(int version)
        {
            return new SplitStorage(Repository, version);
        }

        public int GetVersion()
        {
            if (!Repository.FileExists(".version"))
                return 0;

            int version = 0;
            using (var reader = new StreamReader(Repository.GetRepositoryStream(".version", true)))
            {
                version = Convert.ToInt32(reader.ReadToEnd());
            }

            return version;
        }
    }
}

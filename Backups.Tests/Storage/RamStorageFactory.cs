using Backups.Interfaces;
using Backups.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Tests.Storage
{
    internal class RamStorageFactory : IStorageFactory
    {
        private Dictionary<int, List<FileData>> restorePoints = new Dictionary<int, List<FileData>>();

        public IStorageMode GetStorage(int version)
        {
            List<FileData> data;

            if (restorePoints.ContainsKey(version))
                data = restorePoints[version];
            else
            {
                data = new List<FileData>();
                restorePoints.Add(version, data);
            }

            return new RamStorage(data);
        }

        public int GetVersion()
        {
            if (!restorePoints.Any())
                return 0;

            return restorePoints.Max(pair => pair.Key);
        }
    }
}

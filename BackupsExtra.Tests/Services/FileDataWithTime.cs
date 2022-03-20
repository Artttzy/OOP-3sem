using Backups.Objects;
using System;

namespace BackupsExtra.Tests.Services
{
    public class FileDataWithTime : FileData
    {
        public DateTime CreationTime { get; set; }
    }
}

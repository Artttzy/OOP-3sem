using System;
using Backups.Interfaces;

namespace BackupsExtra.Interfaces
{
    public interface IExtendedRepository : IRepository
    {
        void AppendLine(string fileName, string line);
        void DeleteFile(string fileName);
        DateTime GetFileCreationTime(string fileName);
        void MoveFile(string from, string toLocation);
        string ReadFromFile(string fileName);
        void WriteToFile(string fileName, string content);
    }
}

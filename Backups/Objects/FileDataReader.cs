using System.IO;

namespace Backups.Objects
{
    public class FileDataReader
    {
        public static FileData Read(string path)
        {
            string name = Path.GetFileName(path);
            return new FileData
            {
                Content = File.ReadAllBytes(path),
                FileName = name,
            };
        }
    }
}

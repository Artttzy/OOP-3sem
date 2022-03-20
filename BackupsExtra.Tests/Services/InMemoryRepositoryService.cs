using BackupsExtra.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BackupsExtra.Tests.Services
{
    public class InMemoryRepositoryService : IExtendedRepository
    {
        private static readonly Dictionary<string, FileDataWithTime> files = new Dictionary<string, FileDataWithTime>();

        public void AppendLine(string fileName, string line)
        {
            if (!FileExists(fileName))
            {
                WriteToFile(fileName, string.Empty);
            }

            string content = Encoding.Default.GetString(files[fileName].Content);
            FileDataWithTime tempData = files[fileName];

            files[fileName] = new FileDataWithTime
            {
                FileName = tempData.FileName,
                CreationTime = tempData.CreationTime,
                Content = Encoding.Default.GetBytes(content + Environment.NewLine + line)
            };
        }

        public void DeleteFile(string fileName)
        {
            files.Remove(fileName);
        }

        public IEnumerable<string> EnumerateEntries(string pattern, bool recursive)
        {
            pattern = pattern.Replace(".", @"\.");
            pattern = pattern.Replace("*", ".*");

            var regex = new Regex(pattern);
            return files.Where(f => regex.IsMatch(f.Key)).Select(f => f.Key);
        }

        public bool FileExists(string archiveName)
        {
            return files.ContainsKey(archiveName);
        }

        public DateTime GetFileCreationTime(string fileName)
        {
            return files[fileName].CreationTime;
        }

        public Stream GetRepositoryStream(string archiveName, bool read)
        {
            if (!read)
            {
                if (!files.ContainsKey(archiveName))
                {
                    files.Add(archiveName, new FileDataWithTime
                    {
                        Content = Array.Empty<byte>(),
                        CreationTime = DateTime.UtcNow,
                        FileName = archiveName
                    });
                }

                var ms = new ObservableMemoryStream();
                FileDataWithTime file = files[archiveName];
                ms.Write(file.Content);

                ms.OnDispose += (content) =>
                {
                    lock (files)
                    {
                        files[archiveName] = new FileDataWithTime
                        {
                            FileName = file.FileName,
                            CreationTime = file.CreationTime,
                            Content = content
                        };
                    }
                };

                return ms;
            }
            else
            {
                var ms = new MemoryStream(files[archiveName].Content);
                return ms;
            }
        }

        public void MoveFile(string from, string toLocation)
        {
            files.Add(toLocation, files[from]);
            files.Remove(from);
        }

        public string ReadFromFile(string fileName)
        {
            return Encoding.Default.GetString(files[fileName].Content);
        }

        public void WriteToFile(string fileName, string content)
        {
            if (!FileExists(fileName))
            {
                files.Add(fileName, null);
            }

            files[fileName] = new FileDataWithTime
            {
                CreationTime = DateTime.UtcNow,
                FileName = fileName,
                Content = Encoding.Default.GetBytes(content)
            };
        }
    }
}

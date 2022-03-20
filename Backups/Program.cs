using System;
using System.IO;
using Backups.Objects;
using Backups.Tools;

namespace Backups
{
    internal class Program
    {
        private static void BackupTest(BackupJob job)
        {
            string command;

            while ((command = Console.ReadLine()) != string.Empty)
            {
                if (command == ":commit")
                {
                    job.CreateRestorePoint();
                    continue;
                }

                bool delete = false;

                if (command.StartsWith(":del "))
                {
                    command = command[5..];
                    delete = true;
                }

                if (!delete)
                {
                    job.AddFile(FileDataReader.Read(command));
                }
                else
                {
                    string name = Path.GetFileName(command);
                    job.RemoveFile(new FileData
                    {
                        FileName = name,
                    });
                }
            }
        }

        private static void Main()
        {
            int select;
            Console.Write("Введите вид бэкапа (1 - single; 2 - split): ");
            select = Convert.ToInt32(Console.ReadLine());

            switch (select)
            {
                case 1:
                    BackupTest(new BackupJob("single job", new SingleStorageFactory
                    {
                        Repository = new FileRepository("./BackupsSingle"),
                    }));
                    break;

                case 2:
                    BackupTest(new BackupJob("split job", new SplitStorageFactory
                    {
                        Repository = new FileRepository("./BackupsSplit"),
                    }));
                    break;

                default:
                    break;
            }

            Console.ReadKey();
        }
    }
}

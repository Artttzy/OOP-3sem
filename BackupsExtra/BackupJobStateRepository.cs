using System.Text.Json;
using BackupsExtra.Interfaces;
using BackupsExtra.State;

namespace BackupsExtra
{
    public class BackupJobStateRepository
    {
        private readonly IExtendedRepository repository;

        public BackupJobStateRepository(IExtendedRepository repository)
        {
            this.repository = repository;
        }

        public void Write(BackupJobState state)
        {
            string json = JsonSerializer.Serialize(state);
            repository.WriteToFile("state.json", json);
        }

        public BackupJobState Read()
        {
            string json = repository.ReadFromFile("state.json");
            return JsonSerializer.Deserialize<BackupJobState>(json);
        }
    }
}

using QuickSurprise.Context.Entities;
using System.Text.Json;

namespace QuickSurprise.Services
{
    public class JsonStorageProvider :ICrudService
    {
        private readonly string _filePath;

        public JsonStorageProvider(string filePath)
        {
            _filePath = filePath;
        }

        public List<Person> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Person>();

            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Person>>(jsonData) ?? new List<Person>();
        }

        public Person? GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void Add(Person person)
        {
            var persons = GetAll();
            persons.Add(person);
            SaveToFile(persons);
        }

        public void Update(Person updatedPerson)
        {
            var persons = GetAll();
            var index = persons.FindIndex(p => p.Id == updatedPerson.Id);
            if (index != -1)
            {
                persons[index] = updatedPerson;
                SaveToFile(persons);
            }
        }

        public void Delete(int id)
        {
            var persons = GetAll();
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                persons.Remove(person);
                SaveToFile(persons);
            }
        }

        private void SaveToFile(List<Person> persons)
        {
            var jsonData = JsonSerializer.Serialize(persons, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }
    }
}

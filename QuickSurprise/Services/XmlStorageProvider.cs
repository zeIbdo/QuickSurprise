using QuickSurprise.Context.Entities;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace QuickSurprise.Services
{
    public class XmlStorageProvider :ICrudService
    {
        private readonly string _filePath;

        public XmlStorageProvider(string filePath)
        {
            _filePath = filePath;
        }

        public List<Person> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Person>();

            var xdoc = XDocument.Load(_filePath);
            return xdoc.Root?
                .Elements("person")
                .Select(x => new Person
                {
                    Id = (int)x.Element("id"),
                    Firstname = (string)x.Element("firstname"),
                    Lastname = (string)x.Element("lastname"),
                    Age = (int)x.Element("age")
                })
                .ToList() ?? new List<Person>();
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
            var xdoc = new XDocument(
                new XElement("persons",
                    persons.Select(p => new XElement("person",
                        new XElement("id", p.Id),
                        new XElement("firstname", p.Firstname),
                        new XElement("lastname", p.Lastname),
                        new XElement("age", p.Age)
                    ))
                )
            );
            xdoc.Save(_filePath);
        }
    }
}

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
            if (!File.Exists(_filePath))
                return new List<Person>();

            var xdoc = XDocument.Load(_filePath);
            var root = xdoc.Root;

            if (root == null)
                return new List<Person>();

            var persons = new List<Person>();

            foreach (var x in root.Elements("person"))
            {
                var person = new Person
                {
                    Id = (int)x.Element("id"),
                    Firstname = (string)x.Element("firstname"),
                    Lastname = (string)x.Element("lastname"),
                    Age = (int)x.Element("age")
                };

                persons.Add(person);
            }

            return persons;
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
            foreach (var person in persons)
            {
                if (person.Id == updatedPerson.Id)
                {
                    person.Firstname = updatedPerson.Firstname;
                    person.Lastname = updatedPerson.Lastname;
                    person.Age = updatedPerson.Age;

                    SaveToFile(persons);
                    break;
                }
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


using QuickSurpriseRemake.Helpers;
using QuickSurpriseRemake.Models;
using System.Xml.Serialization;

namespace QuickSurpriseRemake.Services
{
    public class XmlChangeService<T> : IChangeService<T> where T :BaseEntity
    {
        private readonly string _xmlFilePath;

        public XmlChangeService(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        public void Create(T entity)
        {
            var entities = GetAll();
            entities.Add(entity);
            XmlHelper<List<T>>.SerializeToXmlFile(_xmlFilePath, entities);
        }

        public void Delete(int id)
        {
            var entities = GetAll();
            var remove = entities.FirstOrDefault(x=>x.Id == id);
            if (remove != null)
            {
                entities.Remove(remove);
                XmlHelper<List<T>>.SerializeToXmlFile(_xmlFilePath, entities);
            }
            else
            {
                Console.WriteLine("Entity not found ");
                throw new Exception();
            }
        }

        public List<T> GetAll()
        {
            if (!File.Exists(_xmlFilePath))
                return new List<T>();

            var entities = XmlHelper<List<T>>.DeserializeFromXml(_xmlFilePath);

            return entities ?? new List<T>();

        }

        public T? GetById(int id)
        {
            if (!File.Exists(_xmlFilePath))
                return default;

            var entities = XmlHelper<List<T>>.DeserializeFromXml(_xmlFilePath);

            if(entities == null)
                return default;

            return entities.FirstOrDefault(e => e.Id == id);
        }

        public void Update(T entity)
        {
            var entities = GetAll();
            bool flag = false;
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Id == entity.Id)
                {
                    entities[i] = entity;
                    flag = true;
                    break;
                }

            }
            if (flag)
            {
                XmlHelper<List<T>>.SerializeToXmlFile(_xmlFilePath, entities);
            }
            else
            {
                Console.WriteLine("Entity not found");
                throw new Exception();
            }
        }



    }
}

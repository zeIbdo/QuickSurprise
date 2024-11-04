using QuickSurpriseRemake.Helpers;
using QuickSurpriseRemake.Models;

namespace QuickSurpriseRemake.Services
{
    public class JsonChangeService<T> : IChangeService<T> where T : BaseEntity
    {
        private readonly string _jsonFilePath;

        public JsonChangeService(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public void Create(T entity)
        {
            var entities = GetAll();

            entities.Add(entity);

            JsonHelper<List<T>>.SerializeToJsonFile(_jsonFilePath, entities);
        }

        public void Delete(int id)
        {
            var entities = GetAll();
            var delete = entities.FirstOrDefault(e => e.Id == id);
            if (delete != null)
            {
                entities.Remove(delete);
                JsonHelper<List<T>>.SerializeToJsonFile(_jsonFilePath, entities);
            }
            else
            {
                Console.WriteLine("Entity not found for deletion.");
                throw new Exception();      
            }
        }

        public List<T> GetAll()
        {
            if(!File.Exists(_jsonFilePath))
                return new List<T>();

            var entities = JsonHelper<List<T>>.DeserializeFromJsonFile(_jsonFilePath);
             
            return entities ?? new List<T>();
        }

        public T? GetById(int id)
        {
            if (!File.Exists(_jsonFilePath))
                return default;

            var entities = JsonHelper<List<T>>.DeserializeFromJsonFile(_jsonFilePath);

            if(entities == null)
                return default;

            return entities.FirstOrDefault(x=>x.Id==id);
        }

        public void Update(T entity)
        {
            var entities = GetAll();
            bool flag = false;
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Id == entity.Id)
                {
                    entities[i]= entity;
                    flag=true;
                    break;
                }
            }
            if(flag)
                JsonHelper<List<T>>.SerializeToJsonFile(_jsonFilePath , entities);
            else
            {
                Console.WriteLine("Entity not found");
                throw new Exception();
            }
        }
    }
}

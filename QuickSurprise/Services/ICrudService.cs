using QuickSurprise.Context.Entities;

namespace QuickSurprise.Services
{
    public interface ICrudService
    {
        List<Person> GetAll();
        Person? GetById(int id);
        void Add(Person person);
        void Update(Person person);
        void Delete(int id);
    }
}

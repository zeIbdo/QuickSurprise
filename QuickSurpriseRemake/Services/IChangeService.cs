using QuickSurpriseRemake.Models;

namespace QuickSurpriseRemake.Services
{
    public interface IChangeService<T> where T : BaseEntity
    {
        List<T> GetAll();
        T? GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}

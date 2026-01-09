using CaisseEnregistreuse.Models;
namespace CaisseEnregistreuse.Service

{
    public interface IService<T>
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }

}





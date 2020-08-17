using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAPI.Infrastructure.security.repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindAllAsyncWithInclude();
        Task<T> FindByIdAsync(long id);
        T FindById(long id);
        T Save(T entity);
        T Update(T entity);
        void Delete(long id);
        void SaveChanges();
    }

}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using stemnote.app.infrastructure.common.entity;

namespace VideoAPI.Infrastructure.security.repository
{
    public class AbstractRepository<T> where T : class
    {
        protected DataContext context { get; set; }
        protected DbSet<T> entity { get; set; }
        public AbstractRepository(DataContext _context)
        {
            this.context = _context;
            this.entity = context.Set<T>();
        }

        virtual public async Task<List<T>> FindAllAsync()
        {
            return await entity.ToListAsync();
        }
        virtual public async Task<T> FindByIdAsync(long id)
        {
            return await entity.FindAsync(id);
        }
        virtual public T FindById(long id)
        {
            return entity.Find(id);
        }
        public T Save(T _entity)
        {
            // (_entity as BaseEntity).OnBeforeInsert();
            var response = entity.Add(_entity).Entity;
            context.SaveChanges();
            return response;
        }
        public void Delete(long id)
        {
            var response = entity.Remove(entity.Find(id));
            context.SaveChanges();
        }

        public T Update(T _entity)
        {
            // (_entity as BaseEntity).OnBeforeUpdate();
            var response = entity.Update(_entity).Entity;
            context.SaveChanges();
            return response;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
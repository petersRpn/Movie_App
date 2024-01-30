using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Repositories.Interfaces;


namespace Movies.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(List<T> entity)
        {
            dbSet.UpdateRange(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter)
        {

            return dbSet.FirstOrDefault(filter);
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter);
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        
    }
}

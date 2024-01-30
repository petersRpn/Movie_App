using System.Linq.Expressions;

namespace Movies.Core.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(List<T> entity);
        T FirstOrDefault(Expression<Func<T, bool>> filter);
        bool Any(Expression<Func<T, bool>> filter);
    }
}

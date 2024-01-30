using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Repositories.Interfaces;




namespace Movies.Repositories.Implementation;

public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork
  where TContext : AppDbContext, IDisposable
{
  private Dictionary<Type, object> _repositories = new();

  public UnitOfWork(TContext context)
  {
    Context = context ?? throw new ArgumentNullException(nameof(context));
  }

  public TContext Context { get; }

  public void Dispose()
  {
    Context?.Dispose();
  }

  public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
  {
    if (_repositories == null) _repositories = new Dictionary<Type, object>();

    var type = typeof(TEntity);
    if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
    return (IRepository<TEntity>)_repositories[type];
  }


  public int SaveChanges()
  {
    var entries = Context.ChangeTracker.Entries()
      .Where(e => e.Entity is AuditBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

    foreach (var entityEntries in entries)
    {
      ((AuditBase)entityEntries.Entity).UpdatedOn = DateTime.UtcNow;
      if (entityEntries.State == EntityState.Added) ((AuditBase)entityEntries.Entity).CreatedOn = DateTime.UtcNow;
    }

    return Context.SaveChanges();
  }
}
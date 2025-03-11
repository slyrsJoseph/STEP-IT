using GameStoreProject.Data.Context;

namespace GameStoreProject.Data.Repository.MainRep;
using Microsoft.EntityFrameworkCore;



public class Repository<T> : IRepository<T> where T : class
{
    protected readonly GameStoreContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(GameStoreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached) 
        {
            _dbSet.Attach(entity);
        }
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
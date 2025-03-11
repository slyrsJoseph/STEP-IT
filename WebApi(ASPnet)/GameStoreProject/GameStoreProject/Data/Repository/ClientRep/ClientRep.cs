using GameStoreProject.Data.Context;
using GameStoreProject.Data.Repository.MainRep;
using Microsoft.EntityFrameworkCore;

namespace GameStoreProject.Data.Repository.ClientRep;
using GameStoreProject.Data.Models;

public class ClientRep : Repository<User> 
{
    public ClientRep(GameStoreContext context) : base(context)
    {
    }

    public async Task<User> GetById(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<User>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task AddAsync(User entity) => await _dbSet.AddAsync(entity);

    public void Update(User entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _context.Entry(entity).State = EntityState.Modified;
    }
    
    

    public void Delete(User entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    

    public async Task<User> GetClientByEmail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetClientByName(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == name);
    }

    public async Task<List<User>> GetLowInfoClients()
    {
        return await _dbSet.Select(c => new User
        {
            Username = c.Username,
            Email = c.Email,
            CreatedAt = c.CreatedAt,
        }).ToListAsync();
    }
}
    

using Data.Data;
using Microsoft.EntityFrameworkCore;
using Model.Model.Data;

namespace Repositories;

public class UserRepository : BaseRepository, IRepository<User>
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Set<User>().ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _dbContext.Set<User>().FindAsync(id);
    }

    public async Task<User> AddAsync(User entity)
    {
        await _dbContext.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        _dbContext.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(User entity)
    {
        _dbContext.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
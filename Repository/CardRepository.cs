using Data.Data;
using Microsoft.EntityFrameworkCore;
using Model.Model.Data;

namespace Repositories
{
    public class CardRepository : BaseRepository, IRepository<Card>
    {
        public CardRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await _dbContext.Set<Card>().ToListAsync();
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Card>().FindAsync(id);
        }

        public async Task<Card> AddAsync(Card entity)
        {
            await _dbContext.Set<Card>().AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<Card> UpdateAsync(Card entity)
        {
            _dbContext.Set<Card>().Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Card entity)
        {
            _dbContext.Set<Card>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

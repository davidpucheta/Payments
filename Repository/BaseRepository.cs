using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class BaseRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

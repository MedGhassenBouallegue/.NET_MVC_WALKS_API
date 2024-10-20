using Microsoft.EntityFrameworkCore;
using PR.API.Data;
using PR.API.Models.Domain;

namespace PR.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZDbContext dbContext;
        public SQLRegionRepository(NZDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using PR.API.Models.Domain;

namespace PR.API.Data
{
    public class NZDbContext: DbContext
    {
        //"ctor" double tab to do the constructor
        public NZDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }
        public DbSet<Difficulty>   Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

    }
}

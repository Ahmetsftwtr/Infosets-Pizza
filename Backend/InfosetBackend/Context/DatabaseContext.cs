using InfosetBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace InfosetBackend.Context
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        {

        }

        public DbSet<Resturants> restaurant_branches {get;set;}


    }
}

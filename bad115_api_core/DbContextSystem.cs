using Microsoft.EntityFrameworkCore;

namespace bad115_api_core
{
    public class DbContextSystem:DbContext
    {
        public DbContextSystem()
        {

        }

        public DbContextSystem(DbContextOptions<DbContextSystem> options) : base(options)
        {

        }
    }
}

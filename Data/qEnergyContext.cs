using Microsoft.EntityFrameworkCore;
using qenergy.Models;

namespace qenergy.Data
{
    // this will serve as the gateway through which I will interact with the database
    // The constructor accepts a parameter of type DbContextOptions<qEnergyContext>. This allows external code to pass in the configuration, so the same DbContext can be shared between test and production code and even used with different providers.
    // The DbSet<T> properties correspond to tables to be created in the database.
    // The table names will match the DbSet<T> property names in the qEnergyContext class. This behavior can be overridden if needed.
    // When instantiated, qEnergyContext will expose User and Quotes properties. Changes you make to the collections exposed by those properties will be propagated to the database.
    public class qEnergyContext : DbContext
    {
        public qEnergyContext(DbContextOptions<qEnergyContext> options)
            : base(options)
        {
        }

        // Tables will be created by DbSet<type> name = Set<type>()
        public DbSet<User> Users => Set<User>(); // Users table
        public DbSet<Quote> Quotes => Set<Quote>(); // Quotes table
    }

}
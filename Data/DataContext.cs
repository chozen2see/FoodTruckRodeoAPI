using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    // DbContext - represents a session with the db.
    // can be used to query and save instances of our entities
    public class DataContext : DbContext
    {
        // DataContext class needs to be available as a service
        // so it can be consumed by other parts of application
        public DataContext(
            //<type or class>
            DbContextOptions<DataContext> options
            ) : base(options){
            // class we are deriving from's options (DbContext)
            }

        // tell DataContext class about our entities
        // DbSet: by creating a property w/ type of DbSet
        // <Value> is the type of this DbSet
        // Values: table name used during scaffolding

        public DbSet<Value> Values { get; set; }

    }
}
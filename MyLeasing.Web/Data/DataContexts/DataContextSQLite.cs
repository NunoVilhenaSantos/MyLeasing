using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.DataContexts;

public class DataContextSQLite : IdentityDbContext<User>
// public class DataContextSQLite : DbContext
{
    public DataContextSQLite(DbContextOptions<DataContextSQLite> options) :
        base(options)
    {
    }


    public DbSet<Owner> Owners { get; set; }

    public DbSet<Lessee> Lessees { get; set; }
}
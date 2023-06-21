using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.DataContexts;

public class DataContextPostgreSQL : IdentityDbContext<User>
// public class DataContextMSSql : DbContext
{
    public DataContextPostgreSQL(
        DbContextOptions<DataContextPostgreSQL> options) :
        base(options)
    {
    }


    public DbSet<Owner> Owners { get; set; }

    public DbSet<Lessee> Lessees { get; set; }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.DataContexts;

public class DataContextMySql : IdentityDbContext<User>
// public class DataContextMySql : DbContext
{
    public DataContextMySql(DbContextOptions<DataContextMySql> options) :
        base(options)
    {
    }


    public DbSet<Owner> Owners { get; set; }

    public DbSet<Lessee> Lessees { get; set; }
}
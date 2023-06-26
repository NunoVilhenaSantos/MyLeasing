using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.DataContexts;

public class DataContextMSSQL : IdentityDbContext<User>
// public class DataContextMSSQL : DbContext
{
    public DataContextMSSQL(DbContextOptions<DataContextMSSQL> options) :
        base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }

    public DbSet<Lessee> Lessees { get; set; }
}
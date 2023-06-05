using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.DataContexts;

public class DataContext : IdentityDbContext<User>
// public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }

    public DbSet<MyLeasing.Web.Data.Entities.Lessee> Lessee { get; set; } = default!;
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Entities;


namespace MyLeasing.Common.DataContexts;

public class DataContext : IdentityDbContext<User>
// public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }
}
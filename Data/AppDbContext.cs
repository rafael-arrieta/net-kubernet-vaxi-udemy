using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetKubernet.Models;

namespace NetKubernet.Data;

public class AppDbContext : IdentityDbContext<User>
{

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    { // This method generates the shots for build the all tables on DB
        base.OnModelCreating(builder);
    }

    public DbSet<Property>? Properties { get; set; }

}
using Blue.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blue.DataContext
{
    public class BlueDbContext:IdentityDbContext<AppUser>
    {
        public BlueDbContext(DbContextOptions<BlueDbContext> options) : base(options) { }

        public DbSet<Feature> Features { get; set; }

    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShrimpPond.API.Authorization.Models;

namespace ShrimpPond.API.Authorization.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using static WebApplication5.Data.AppDBContext;
using static WebApplication5.Models.Purshes;

namespace WebApplication5.Data
{
	public class AppDBContext : IdentityDbContext<User>
    {

 
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Purshes> Purshes { get; set; } = null!;
        public DbSet<GameKeys> GameKeys { get; set; } = null!;
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            Database.EnsureCreated();
        }



       

    }
}

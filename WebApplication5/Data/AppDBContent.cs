using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
	public class AppDBContent : DbContext
	{

        public DbSet<Game> Games { get; set; } = null!;
        public AppDBContent(DbContextOptions<AppDBContent> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        
    }
}

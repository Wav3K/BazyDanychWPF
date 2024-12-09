using Microsoft.EntityFrameworkCore;

namespace BazyDanych4g1;

public class dbContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=wpf4g1;Trusted_Connection=True;");
    }
}
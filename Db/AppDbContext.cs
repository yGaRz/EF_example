using EF_example.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_example.Db;

public class AppDbContext:DbContext
{
    public  DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}

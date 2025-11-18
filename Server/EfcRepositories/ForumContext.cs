using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class ForumContext : DbContext{
    public DbSet<User> Users{ get; set; }
    public DbSet<Post> Posts{ get; set; }
    public DbSet<Comment> Comments{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlite("Data Source = forum.db");
    }
}
using Domain;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class RedditContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Subreddit> Subreddits { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = C:\\Users\\laura\\OneDrive\\Documents\\GitHub\\LAURA_DNP_A3_2\\EfcDataAccess\\Reddit.db");
    }
    
}
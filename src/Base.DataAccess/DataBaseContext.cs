using Base.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Base.DataAccess;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class DataBaseContext : DbContext
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<RefreshToken> RefreshTokens { get; set; }
    internal DbSet<Event> Events { get; set; }
    internal DbSet<Category> Categories { get; set; }


    internal DbSet<EventToCategory> EventToCategory { get; set; }
    internal DbSet<EventToUser> EventToUser { get; set; }
    internal DbSet<UserToCategory> UserToCategory { get; set; }


    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

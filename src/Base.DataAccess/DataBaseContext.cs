using Base.Core;
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

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

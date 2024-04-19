using Microsoft.EntityFrameworkCore;
using Base.DataAccess.Dto;
using System.Reflection;

namespace Base.DataAccess;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class DataBaseContext : DbContext
{
    internal DbSet<UserDto> Users { get; set; }
    internal DbSet<RefreshTokenDto> RefreshTokens { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

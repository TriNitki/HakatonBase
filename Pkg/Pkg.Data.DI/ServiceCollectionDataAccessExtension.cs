using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pkg.Data.DI;

/// <summary>
/// Расширение для <see cref="IServiceCollection"/>, добавляющее конфигурацию доступа к данным.
/// </summary>
public static class ServiceCollectionDataAccessExtension
{
    /// <summary>
    /// Провайдер БД
    /// </summary>
    private const string _dbProvider = "Npgsql";

    /// <summary>
    /// Добавить контекст БД
    /// </summary>
    /// <typeparam name="TContext"> Контекст </typeparam>
    /// <param name="services"> Коллекция сервисов </param>
    /// <param name="cfg"> Конфигурация приложения </param>
    /// <returns> Коллекция сервисов </returns>
    /// <exception cref="ArgumentNullException"> Не задана строка подключения к БД </exception>
    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, IConfiguration cfg)
        where TContext : DbContext
    {
        var connectionString = cfg.GetConnectionString(_dbProvider)
            ?? throw new ArgumentNullException(null, "Не задана строка подключения к БД");

        return services.AddDbContext<TContext>(options =>
        {
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Base.Service"));
            options.UseSnakeCaseNamingConvention();
        });
    }
}

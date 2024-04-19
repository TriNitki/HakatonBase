using Pkg.Data.DI;
using Base.Integration;
using Base.DataAccess;
using Base.Core.Options;
using Base.Service.Options;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Login;
using MNX.SecurityManagement.DataAccess.Repositories;
using MNX.SecurityManagement.Service.Infrastructure;
using System.Reflection;
using MNX.SecurityManagement.DataAccess;

namespace Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = ConfigureApp(args);
            await RunApp(builder);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Настроить приложение.
    /// </summary>
    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();

        var services = builder.Services;
        var cfg = builder.Configuration;

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddHealthChecks();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddJwtBearerAuthentication(
            cfg.GetSection(nameof(SecurityOptions))[nameof(SecurityOptions.SecretKey)]!);

        AddAuthorization(services, cfg);

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        services.AddSwagger(xmlFilePath);

        ConfigureDI(services, cfg);

        return builder;
    }

    /// <summary>
    /// Настроить DI контейнер.
    /// </summary>
    private static void ConfigureDI(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAutoMapper(typeof(DbMappingProfile).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));
        services.AddDataContext<DataBaseContext>(configuration);

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();
    }

    /// <summary>
    /// Добавить необходимые для авторизации сервисы в DI
    /// </summary>
    private static void AddAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        // authorized user
        services.AddHttpContextAccessor();

        var securityOptionsSection = configuration.GetSection(nameof(SecurityOptions));
        services.Configure<SecurityOptions>(x =>
        {
            x.SecretKey = securityOptionsSection[nameof(SecurityOptions.SecretKey)]
                            ?? throw new ArgumentNullException(null, "Не задан секретный ключ");

            x.AccessTokenLifetimeInMinutes
                = securityOptionsSection
                    .GetValue(nameof(SecurityOptions.AccessTokenLifetimeInMinutes), 15);

            x.RefreshTokenLifetimeInMinutes
                = securityOptionsSection
                    .GetValue(nameof(SecurityOptions.RefreshTokenLifetimeInMinutes), 180);
        });

        services.Configure<PasswordOptions>(x =>
        {
            x.Salt = securityOptionsSection.GetSection(nameof(PasswordOptions))[nameof(PasswordOptions.Salt)]
                  ?? throw new ArgumentNullException(null, "Не задана соль для шифрования пароля.");
        });
    }

    /// <summary>
    /// Запустить приложение.
    /// </summary>
    private static async Task RunApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var appName = builder.Configuration["ServiceName"]
            ?? throw new ArgumentNullException(null, "Не указано название сервиса");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataBaseContext>();
            context.Database.EnsureCreated();
        }

        app.UseRouting();
        app.UseCors();

        app.MapHealthChecks("/health").AllowAnonymous();
        app.MapGet(string.Empty, async ctx => await ctx.Response.WriteAsync(appName)).AllowAnonymous();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}

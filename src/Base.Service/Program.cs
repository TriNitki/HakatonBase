using Pkg.Data.DI;
using Base.DataAccess;
using Base.Service.Options;
using Base.UseCases.Abstractions;
using MNX.SecurityManagement.DataAccess;
using AutoMapper;
using Base.Core.Providers;
using Base.Service.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Base.UseCases;
using Base.DataAccess.Repositories;
using Base.Core.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Base.UseCases.Commands.Auth.Login;

namespace Service;

/// <inheritdoc/>
public class Program
{
    /// <inheritdoc/>
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

    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        var cfg = builder.Configuration;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthorization();

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

        AddJwtBearerAuthentication(services, cfg);

        ConfigureDI(services, builder.Configuration);

        return builder;
    }

    private static void ConfigureDI(IServiceCollection services, ConfigurationManager cfg)
    {
        services.AddAutoMapper(typeof(DbMappingProfile).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));

        services.AddDataContext<DataBaseContext>(cfg);
        services.AddMemoryCache();

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthUserAccessor, AuthUserAccessor>();

        services.ConfigureOptions<SwaggerGenOptionsSetup>();

        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<IPasswordHashProvider, PasswordHashProvider>();

        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfiles(provider.GetService<IPasswordHashProvider>() ??
                throw new ArgumentNullException("IPasswordHashProvider", "Отсутсвует реализация IPasswordHashProvider")));
        }).CreateMapper());
    }

    /// <summary>
    /// Добавить необходимые для аутентификации сервисы в DI
    /// </summary>
    private static void AddJwtBearerAuthentication(IServiceCollection services, ConfigurationManager cfg)
    {
        string securityKey = cfg.GetSection(nameof(SecurityOptions))[nameof(SecurityOptions.SecretKey)]!;

        if (string.IsNullOrWhiteSpace(securityKey))
        {
            throw new ArgumentException("Не задан ключ безопасности", nameof(securityKey));
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // ключ безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = false,
                    // будет ли валидироваться время существования
                    ValidateLifetime = false,
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = false,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = false,
                };
            });

        var securityOptionsSection = cfg.GetSection(nameof(SecurityOptions));
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

    private static async Task RunApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var appName = builder.Configuration["ServiceName"]
            ?? throw new ArgumentNullException("ServiceName", "Не указано название сервиса");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHealthChecks("/health");
        app.MapGet(string.Empty, async ctx => await ctx.Response.WriteAsync(appName));

        await app.RunAsync();
    }
}

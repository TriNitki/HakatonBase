using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Base.Clients;
using Base.Contracts;
using Refit;
using System.Text;
using Dodo.HttpClientResiliencePolicies;

namespace Base.Integration;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/> для интеграции аутентификации.
/// </summary>
public static class AuthenticationServiceCollectionExtensions
{
    /// <summary>
    /// Добавить jwt bearer аутентификацию в DI контейнер.
    /// </summary>
    /// <param name="services"> Коллекция сервисов. </param>
    /// <param name="securityKey"> Ключ безопасности. </param>
    /// <param name="jwtBearerEvents"> События от <see cref="JwtBearerHandler"/>. </param>
    /// <returns> Коллекция сервисов. </returns>
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,
                                                                string securityKey,
                                                                JwtBearerEvents? jwtBearerEvents = null)
    {
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

                    if (jwtBearerEvents != null)
                    {
                        options.Events = jwtBearerEvents;
                    }
                });

        return services;
    }

    /// <summary>
    /// Добавить Сваггер, сконфигурированный для аутентификации веб-клиента.
    /// </summary>
    /// <param name="services"> Сервисы. </param>
    /// <param name="xmlFilePath"> Путь до файла с xml документацией. </param>
    /// <returns> Сервисы. </returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFilePath)
    {
        services.AddSwaggerGen(opts =>
        {
            

            opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = @"Enter access token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        },
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    /// <summary>
    /// Добавить необходимые сервисы и параметры в DI для авторизации системного пользователя.
    /// </summary>
    /// <param name="services"> Сервисы. </param>
    /// <param name="configuration"> Конфигурация. </param>
    /// <returns> Сервисы. </returns>
    private static IServiceCollection AddSystemUserAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var securityServiceName = configuration.GetSecurityServiceName();

        var systemUserOptions = configuration.GetSection(nameof(SystemUserOptions));
        if (string.IsNullOrEmpty(systemUserOptions[nameof(SystemUserOptions.Login)]))
        {
            throw new ArgumentException("Не указан логин для системного пользователя.");
        }

        if (string.IsNullOrEmpty(systemUserOptions[nameof(SystemUserOptions.Password)]))
        {
            throw new ArgumentException("Не указан пароль для системного пользователя.");
        }

        services.AddSingleton<ISystemUserTokenProvider, SystemUserTokenProvider>();
        services.AddTransient<AuthHeaderHandler>();
        services.AddRefitClient<IAuthorizationClient>()
                .ConfigureHttpClient(x => x.BaseAddress = configuration.GetServiceUri(securityServiceName))
                .AddResiliencePolicies();

        services.Configure<SystemUserOptions>(configuration.GetSection(nameof(SystemUserOptions)));

        services.Configure<JwtTokensOptions>(x =>
        {
            x.AccessTokenExpirationInMinutes
                = configuration.GetValue(nameof(JwtTokensOptions.AccessTokenExpirationInMinutes), 15);
        });

        return services;
    }

    /// <summary>
    /// Получить Uri адрес сервиса.
    /// </summary>
    /// <param name="configuration"> Конфигурация. </param>
    /// <param name="serviceName"> Название сервиса. </param>
    /// <returns> Uri адрес сервиса. </returns>
    /// <exception cref="ArgumentException"> Не задан <see cref="AuthEnvironmentVariables.FabioUrl"/>. </exception>
    private static Uri? GetServiceUri(this IConfiguration configuration, string serviceName)
    {
        if (!Uri.TryCreate(configuration[AuthEnvironmentVariables.FabioUrl], UriKind.Absolute, out var fabioUrl))
        {
            throw new ArgumentException($"Не задан {AuthEnvironmentVariables.FabioUrl}");
        }

        return new Uri(fabioUrl, serviceName);
    }

    /// <summary>
    /// Получить адрес сервиса безопасности.
    /// </summary>
    /// <param name="configuration"> Конфигурация. </param>
    /// <returns> Адрес сервиса безопасности </returns>
    /// <exception cref="ArgumentException"> Не указан адрес сервиса безопасности. </exception>
    private static string GetSecurityServiceName(this IConfiguration configuration)
    {
        var securityServiceName = configuration[AuthEnvironmentVariables.SecurityServiceName];

        if (string.IsNullOrEmpty(securityServiceName))
        {
            throw new ArgumentException("Не указан адрес сервиса безопасности.");
        }

        return securityServiceName;
    }
}

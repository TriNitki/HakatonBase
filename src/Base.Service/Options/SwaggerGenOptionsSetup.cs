using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Base.Service.Options;

/// <inheritdoc/>
public class SwaggerGenOptionsSetup : IPostConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc/>
    public void PostConfigure(string? name, SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath, true);

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"Enter access token",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                },
                new List<string>()
            }
        });
    }
}

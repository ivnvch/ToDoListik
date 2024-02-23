using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using ToDoList.Domain.Settings;

namespace ToDoList.Api
{
    public static class Startup
    {

        public static void AddAuthenticationAndAuthrization(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
                    var jwtKey = options.JwtKey;
                    var issuer = options.Issuer;
                    var audience = options.Audience;

                    o.Authority = options.Authority;
                    o.RequireHttpsMetadata = false;

                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });
        }


        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddApiVersioning().AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;//если не указывается версия api, тогда береётся версия по умолчанию(1.0)
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "ToDoListik.API",
                    Description = "This is version 1.0",
                    TermsOfService = new Uri("https://habr.com/ru/companies/microsoft/articles/325872/"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Project",
                        Email = "tarasowets33@mail.ru"
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Version = "v2",
                    Title = "ToDoListik.API",
                    Description = "This is version 2.0",
                    TermsOfService = new Uri("https://habr.com/ru/companies/microsoft/articles/325872/"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Project",
                        Email = "tarasowets33@mail.ru"
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        Array.Empty<string>()
                    }
                });

                //var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            });
        }
    }
}

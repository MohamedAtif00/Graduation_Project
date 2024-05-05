using Graduation_Project.Application.DTOs.Authentication;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Repsitory.RefreshTokenRepo;
using Graduation_Project.Domain.Repsitory.TrainerRepo;
using Graduation_Project.Domain.Repsitory.UserRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;
using Graduation_Project.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Graduation_Project.Infrastructure
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("default")));


            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
            {
                // Configure password requirements
                options.Password.RequiredLength = 8; // Minimum password length
                options.Password.RequireDigit = false; // Requires at least one digit
                options.Password.RequireLowercase = false; // Requires at least one lowercase letter
                options.Password.RequireUppercase = false; // Requires at least one uppercase letter
                options.Password.RequireNonAlphanumeric = false; // Requires at least one non-alphanumeric character

            }).AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            var jwtSettings = configuration.GetSection("JwtSettings");

            var setting = services.Configure<JwtSettings>(jwtSettings);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.ClaimsIssuer = jwtSettings["Issuer"];
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SigningKey"])),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audiance"]

                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("IS_TOKEN_EXPIRED", "Y");
                        return Task.CompletedTask;

                    }
                };

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<ITrainerRatingRepository, TrainerRatingRepository>();

            return services;
        }
    }
}

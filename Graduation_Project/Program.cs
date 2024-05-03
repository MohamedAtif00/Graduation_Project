
using Graduation_Project.Application;
using Graduation_Project.Infrastructure;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddInfrastructure(configuration).AddApplication();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();

                var userMgr = services.GetRequiredService<UserManager<IdentityUser<Guid>>>();
                var roleMgr = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                SeedData.Initialize(context, userMgr, roleMgr).Wait();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            

            app.Run();
        }
    }
}

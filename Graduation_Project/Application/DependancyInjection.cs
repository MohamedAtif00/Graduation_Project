using Graduation_Project.Application.Abstraction;
using Graduation_Project.Application.Services;

namespace Graduation_Project.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly);

            });



            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}

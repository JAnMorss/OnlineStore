using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

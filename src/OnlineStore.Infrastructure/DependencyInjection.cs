using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Infrastructure.Clock;
using OnlineStore.Infrastructure.Email;
using OnlineStore.Infrastructure.Repositories;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Domain.Users.Interface;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.Application.Clock;
using OnlineStoreAPI.Shared.Kernel.Application.Email;

namespace OnlineStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddTransient<IEmailService, EmailService>();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'Database' is missing in configuration.");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                });
            });


            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

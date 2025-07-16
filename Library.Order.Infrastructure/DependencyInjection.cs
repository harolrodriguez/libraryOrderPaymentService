using Library.Order.Application.Interfaces;
using Library.Order.Infrastructure.PaymentGateways;
using Library.Order.Infrastructure.Persistence;
using Library.Order.Infrastructure.Persistence.Repositories;
using Library.Order.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentGatewayService, MockPaymentService>();
            services.AddSingleton<IProductService, MockProductService>();

            return services;
        }
    }

    public interface IOrderRepository
    {
    }
}
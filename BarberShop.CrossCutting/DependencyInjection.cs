using BarberShop.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection AddBarberShopServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfraServices(configuration.GetConnectionString("DefaultConnection") ?? string.Empty);

        return services;
    }
}
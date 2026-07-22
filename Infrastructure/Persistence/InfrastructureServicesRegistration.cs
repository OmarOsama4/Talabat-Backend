using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return Services;
        }
    }
}

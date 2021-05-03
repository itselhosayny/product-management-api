using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.UOW;
using ProductManagement.Infrastructure.Repositories;
using ProductManagement.Infrastructure.UOW;

namespace ProductManagement.Infrastructure
{
    public static class InfrastructureDependenciesExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

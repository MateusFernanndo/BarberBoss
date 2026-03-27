using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Infrastructure.DataAcess;
using BarberBoss.Infrastructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBillingWriteOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingReadOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingsUpdateOnlyRepository, BillingsRepository>();

    }

    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<BarberBossDBContext>(config => config.UseMySql(connectionString, serverVersion));
    }

    
}

using JetKasa.Infrastructure.Repositories;
using JetKasa.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GenericRepository;
using Scrutor;
using JetKasa.Infrastructure.Context;

namespace JetKasa.Infrastructure
{
    public static class InfrastructureRegistrar
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                string connection = configuration.GetConnectionString("PostgreSql")!;
                opt.UseNpgsql(connection);
            });

            // UnitOfWork
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());

            // Repositories
            services.Scan(opt => opt
          .FromAssemblies(typeof(InfrastructureRegistrar).Assembly)
          .AddClasses(publicOnly: false)
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsImplementedInterfaces()
          .WithScopedLifetime()
          );

            return services;
        }
    }
}

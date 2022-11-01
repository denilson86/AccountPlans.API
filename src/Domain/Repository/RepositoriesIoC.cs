using Domain.Interfaces;
using Domain.Rules;
using Infra.Data;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Repository
{
    [ExcludeFromCodeCoverage]
    public static class RepositoriesIoC
    {
        public static IServiceCollection AddRepositoriesIoC(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("SqlServer").Value;
            services.AddScoped<IUnitOfWork>(_ => new UnitOfWork(connectionString));
            services.AddScoped<IAccountPlanRepository, AccountPlanRepository>();
            services.AddScoped<IAccountPlansRules, AccountPlansRules>();

            return services;
        }
    }
}

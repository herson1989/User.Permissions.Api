using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Domain.Interfaces.UnitOfWork;
using User.Permissions.Domain.Interfaces.UseCases;
using User.Permissions.Domain.UseCases;
using User.Permissions.Infrastructure.Persistence;
using User.Permissions.Infrastructure.Repositories;

namespace User.Permissions.Api.Extensions
{
    public static class DependenciesExtension
    {
        public static IServiceCollection AddDependenciesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IKafkaProducerRepository, KafkaProducerRepository>();

            //CaseUses
            services.AddTransient<ICreatePermissionUseCase, CreatePermissionUseCase>();
            services.AddTransient<IGetAllPermissionsUseCase, GetAllPermissionsUseCase>();
            services.AddTransient<IUpdatePermissionUseCase, UpdatePermissionUseCase>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

using User.Permissions.Application.Permissions.Create;

namespace User.Permissions.Api.Extensions
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddMediatRExtension(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreatePermissionCommand).Assembly);
            });

            return services;
        }
    }
}

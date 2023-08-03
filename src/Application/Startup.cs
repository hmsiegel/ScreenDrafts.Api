namespace ScreenDrafts.Api.Application;
public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddTransient<ICastAndCrewService, CastAndCrewService>();

        return services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            })
            .AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
    }
}

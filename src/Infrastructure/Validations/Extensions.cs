namespace ScreenDrafts.Api.Infrastructure.Validations;
public static class Extensions
{
    public static IServiceCollection AddBehaviors(this IServiceCollection services, Assembly assemblyContainingValidators) =>
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
}

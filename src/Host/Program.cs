[assembly: ApiConventionType(typeof(ScreenDraftsApiConvention))]

StaticLogger.EnsureInitialized();
Log.Information("Server booting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations().RegisterSerilog();
    builder.Services.AddPersistence();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.RegisterServices();
    builder.Services.AddApplication();
    builder.Services.AddPresentation();

    var app = builder.Build();

    await app.Services.InitializeDatabasesAsync();

    app.UseInfrastructure(builder.Configuration);
    app.MapEndpoints();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception.");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server shutting down...");
    Log.CloseAndFlush();
}
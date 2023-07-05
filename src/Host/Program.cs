[assembly: ApiConventionType(typeof(ScreenDraftsApiConvention))]

StaticLogger.EnsureInitialized();
Log.Information("Server booting up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.RegisterSerilog();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddPersistence(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        await app.Services.InitializeDatabasesAsync();
    }

    app.UsePresentation();
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
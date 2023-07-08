[assembly: ApiConventionType(typeof(ScreenDraftsApiConvention))]

StaticLogger.EnsureInitialized();
Log.Information("Server booting up...");
var builder = WebApplication.CreateBuilder(args);

builder.RegisterSerilog();
builder.Services.RegisterServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence();
builder.Services.AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.Services.InitializeDatabasesAsync();
}

app.UseInfrastructure(builder.Configuration);
app.MapEndpoints();

app.Run();

// try
// {
// }
// catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
// {
//     StaticLogger.EnsureInitialized();
//     Log.Fatal(ex, "Unhandled exception.");
// }
// finally
// {
//      StaticLogger.EnsureInitialized();
//     Log.Information("Server shutting down...");
//      Log.CloseAndFlush();
// }
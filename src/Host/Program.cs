[assembly: ApiConventionType(typeof(ScreenDraftsApiConvention))]

var builder = WebApplication.CreateBuilder(args);

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

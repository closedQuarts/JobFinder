using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot config dosyas�n� y�kle
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Ocelot servisini ekle
builder.Services.AddOcelot();

var app = builder.Build();

// Ocelot pipeline'�n� �al��t�r
await app.UseOcelot();

app.Run();

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot config dosyasýný yükle
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Ocelot servisini ekle
builder.Services.AddOcelot();

var app = builder.Build();

// Ocelot pipeline'ýný çalýþtýr
await app.UseOcelot();

app.Run();

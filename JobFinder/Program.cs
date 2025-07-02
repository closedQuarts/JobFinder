using JobFinder.Infrastructure;
using JobFinder.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IJobSearchRepository, JobSearchRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddScoped<JobAlertService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
//builder.SeerviceBurvices.AddHostedService<SsBackgroundService>();  service bus için arka plan servisi ama her seferinde 500.30 hatası verdi 

// Redis Bağlantısı
var redisConnectionString = builder.Configuration.GetSection("Redis").GetValue<string>("ConnectionString");

if (string.IsNullOrWhiteSpace(redisConnectionString))
{
    throw new Exception("Redis bağlantı dizesi boş veya null! Lütfen appsettings.json veya Azure Configuration'a ekleyin.");
}

var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddSingleton<RedisCacheService>();

// Service Bus Publisher
builder.Services.AddSingleton<ServiceBusPublisher>();
builder.Services.AddSingleton<ServiceBusConsumer>();


// Database Context
builder.Services.AddDbContext<JobFinderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
/*
var consumer = app.Services.GetRequiredService<ServiceBusConsumer>();

_ = consumer.ReceiveMessagesAsync();
*/

app.Run();

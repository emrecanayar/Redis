using Microsoft.EntityFrameworkCore;
using RealTimeNotificationApp.Application.Services.Notifications;
using RealTimeNotificationApp.Application.Services.Repositories;
using RealTimeNotificationApp.BackgroundServices;
using RealTimeNotificationApp.Hubs;
using RealTimeNotificationApp.Persistence.Contexts;
using RealTimeNotificationApp.Persistence.Repositories;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("Redis:ConnectionString");
});


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("Redis:ConnectionString")));


builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationManager>();
builder.Services.AddHostedService<RedisSubscriberService>();
builder.Services.AddDbContext<RealTimeNotificationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));



var app = builder.Build();


// Geliştirme ortamı için Swagger'i ekleyin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // HTTPS yönlendirmesini ekleyin

app.UseRouting();  // Routing'i etkinleştirin

app.UseCors();

app.UseAuthorization();  // Yetkilendirmeyi ekleyin

app.MapControllers();  // Controller endpoint'lerini map edin

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");  // SignalR hub'ını tanımlayın
});

app.Run();
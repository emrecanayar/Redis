var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Microsoft.Extensions.Caching.StackExchangeRedis kütüphanesini indirdikten sonra AddStackExchangeRedisCache servisini projemize dahil ediyoruz. Parametre olarakta redis sunucusnun adresini belirtiyoruz.
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

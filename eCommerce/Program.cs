using eCommerce.Application.Services.Cache;
using eCommerce.Application.Services.Products;
using eCommerce.Application.Services.Repositories;
using eCommerce.Persistence.Context;
using eCommerce.Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ECommerceDbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICacheService, CacheManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

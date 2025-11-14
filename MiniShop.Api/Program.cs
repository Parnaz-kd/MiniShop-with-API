using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniShop.Api.Application.DTOs;
using MiniShop.Api.Application.Services.Implementations;
using MiniShop.Api.Application.Services.Implementations.Policies;
using MiniShop.Api.Application.Services.Interfaces;
using MiniShop.Api.Application.Validation;
using MiniShop.Api.Domain.Abstractions;
using MiniShop.Api.Infrastructure.Persistence;
using MiniShop.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<MiniShopDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

services.AddHttpClient("PublicApiClient", c =>
{
    c.BaseAddress = new Uri("https://example.org/");
});

services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();
services.AddScoped<IValidator<CreateOrderDto>, CreateOrderValidator>();

services.AddSingleton<ITaxPolicy>(new VatTaxPolicy(0.09m));
services.AddTransient<IPriceRule>(_ => new PercentageDiscountRule(0.05m));
services.AddTransient<IPriceRule, CouponDiscountRule>();
services.AddTransient<IPriceCalculator, PriceCalculator>();

services.AddSingleton<ICacheService, CacheService>();

services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseMiddleware<RequestTimingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MiniShopDbContext>();
    await SeedData.EnsureSeedAsync(db);
}

app.Run();


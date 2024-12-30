using AutoMapper;
using Catalog.Api.Data;
using Catalog.Api.Repositories;
using Catalog.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using Sqids;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICatalogDbContext>(d => new CatalogDbContext(builder.Configuration));

builder.Services.AddScoped<ICatalogWriteOnly, CatalogRepository>();
builder.Services.AddScoped<ICatalogReadOnly, CatalogRepository>();

builder.Services.AddSingleton(new SqidsEncoder<long>(new SqidsOptions()
{
    Alphabet = builder.Configuration.GetValue<string>("sqids:alphabet")!,
    MinLength = builder.Configuration.GetValue<int>("sqids:minLength")
}));


builder.Services.AddScoped(d => new MapperConfiguration(cf =>
{
    cf.AddProfile(new MapperService());
}).CreateMapper());

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

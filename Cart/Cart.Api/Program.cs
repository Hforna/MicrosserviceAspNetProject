using Cart.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(d => {
    d.Configuration = builder.Configuration.GetConnectionString("CacheConnection");
});

builder.Services.AddScoped<ICartReadOnly, CartRepository>();
builder.Services.AddScoped<ICartWriteOnly, CartRepository>();

builder.Services.AddHttpClient("catalog", d =>
{
    d.BaseAddress = new Uri("https://catalog:8085");
});

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

using Btb.Api;
using Btb.Api.Source.Domain.Entities;
using Btb.Api.Source.Infrastucture.DataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<DataContext>(options => options
        .UseSqlServer(builder.Configuration.GetSection("AppSettings").Get<AppSettings>().ConnectionString));
builder.Services.Configure<AppSettings>(options => builder.Configuration
        .GetSection("AppSettings")
        .Bind(options));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataProiderFactory>();
builder.Services.AddScoped<EntityFrameworkDataProider>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

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

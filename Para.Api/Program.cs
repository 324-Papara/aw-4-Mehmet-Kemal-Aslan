using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Para.Api.Middlewares;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Mapper;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionStringMsSql = builder.Configuration.GetConnectionString("MsSqlConnection");
builder.Services.AddDbContext<ParaMsSqlDbContext>(options =>
options.UseSqlServer(connectionStringMsSql));

//var connectionStringPostgre = builder.Configuration.GetConnectionString("PostgresSqlConnection");
//builder.Services.AddDbContext<ParaPostgreDbContext>(options =>
//options.UseNpgsql(connectionStringPostgre));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MapperConfig());
});
builder.Services.AddSingleton(config.CreateMapper());

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LogMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using FluentAssertions.Common;
using IncedoInvest.Application.Handlers.CommandHandlers;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using IncedoInvest.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(typeof(Program));


builder.Services.AddMediatR(typeof(LoginAdvisorHandler).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(typeof(RegisterAdvisorHandler).GetTypeInfo().Assembly);

builder.Services.AddDbContext<AdvisorDbContextClass>(
    options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("IncedoInvest.Infrastructure"));
    });

builder.Services.AddScoped<IAdvisorRepository, AdvisorRepository>();
builder.Services.AddMediatR(typeof(Program));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

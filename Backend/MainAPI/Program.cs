using Application_Layer;
using DomainLayer.Common;
using DomainLayer.Interfaces;
using DomainLayer.Models.Auth;
using MainAPI.Configurations;
using MainAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using ISession = DomainLayer.Interfaces.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSystemd();

builder.Services.AddApplicationServices();

//builder.Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PGSQLConnection")));
//builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IContext,DataContext>();
builder.Services.AddControllers();

builder.Services.AddScoped<ISession,Session>();

// Authn / Authrz
builder.Services.AddAuthSetup(builder.Configuration);

// Swagger
builder.Services.AddSwaggerSetup();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwaggerSetup();
app.UseStaticFiles();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
   .RequireAuthorization();

app.Run();

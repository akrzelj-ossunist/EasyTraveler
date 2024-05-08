﻿// using lab05.Repositories;
// using lab05.Services;
// using lab05.Controllers;
using Microsoft.EntityFrameworkCore;
using ET.DataAccess.Persistence;
using Microsoft.AspNetCore.Identity;
using ET.Application.Services;
using ET.Application.Services.Impl;
using ET.DataAccess.Repositories.Impl;
using ET.DataAccess.Repositories;
using ET.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.
builder.Services.AddRazorPages();

/* SERVICES DEPENDENCY INJECTION */
builder.Services.AddScoped<ET.Application.Services.UserService, UserServiceImpl>();

/* MAPPER */
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<DatabaseContext>();

/* REPOSITORIES DEPENDENCY INJECTION */
builder.Services.AddScoped<UserRepository, UserRepositoryImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

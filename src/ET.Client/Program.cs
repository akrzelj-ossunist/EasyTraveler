using Microsoft.EntityFrameworkCore;
using ET.DataAccess.Persistence;
using ET.Application.Services;
using ET.Application.Services.Impl;
using ET.DataAccess.Repositories.Impl;
using ET.DataAccess.Repositories;
using ET.Application.Mappers;
using ET.Application.Utilities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

/* MAPPER DEPENDENCY INJECTION */
builder.Services.AddScoped<UserMapper>();

/* DATABASE CONTEXT DEPENDENCY INJECTION  */
builder.Services.AddScoped<DatabaseContext>();

/* JWT TOKEN DEPENDENCY INJECTION */
builder.Services.AddScoped<JwtService>();

/* SERVICES DEPENDENCY INJECTION */
builder.Services.AddScoped<UserService, UserServiceImpl>();
builder.Services.AddScoped<CompanyService, CompanyServiceImpl>();

/* REPOSITORIES DEPENDENCY INJECTION */
builder.Services.AddScoped<UserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<CompanyRepository, CompanyRepositoryImpl>();

/* IHTTPCONTEXT ACCESSOR DEPENDENCY INJECTION */
builder.Services.AddHttpContextAccessor();

/* AUTHENTICATE USER DEPENDENCY INJECTION */
builder.Services.AddScoped<AuthenticateUser>();

builder.Services.AddSession();

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

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

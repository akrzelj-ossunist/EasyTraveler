using FluentValidation.AspNetCore;
using ET.API;
using ET.API.Filters;
using ET.API.Middleware;
using ET.Application;
using ET.DataAccess;
using ET.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using ET.Application.Mappers;
using ET.Application.Utilities;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidateModelAttribute))
);

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSwagger();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserMapper> ();
builder.Services.AddScoped<CompanyMapper>();
builder.Services.AddScoped<BusMapper>();
builder.Services.AddScoped<RouteMapper>();
builder.Services.AddScoped<LocationMapper>();
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<AuthenticateUser>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using var scope = app.Services.CreateScope();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ET V1"); });

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

namespace ET.API
{
    public partial class Program { }
}

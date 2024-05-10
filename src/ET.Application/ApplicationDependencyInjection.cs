using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ET.Application.Services.Impl;
using ET.Shared.Services;
using ET.Shared.Services.Impl;
using ET.Application.Mappers;
using ET.DataAccess.Repositories.Impl;
using ET.DataAccess.Repositories;
using ET.DataAccess.Persistence;
using ET.Application.Services;


namespace ET.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<UserService, UserServiceImpl>();
        services.AddScoped<CompanyService, CompanyServiceImpl>();
    }
}

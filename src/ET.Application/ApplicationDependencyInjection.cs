﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ET.Application.Common.Email;
using ET.Application.MappingProfiles;
using ET.Application.Services;
using ET.Application.Services.DevImpl;
using ET.Application.Services.Impl;
using ET.Shared.Services;
using ET.Shared.Services.Impl;
using ET.Application.Mappers;
using ET.DataAccess.Repositories.Impl;
using ET.DataAccess.Repositories;
using ET.DataAccess.Persistence;

namespace ET.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        //services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        //services.AddScoped<ITodoListService, TodoListService>();
        //services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IUserService, Services.Impl.UserService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<ITemplateService, TemplateService>();

        if (env.IsDevelopment())
            services.AddScoped<IEmailService, DevEmailService>();
        else
            services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<DatabaseContext>();
        /*My Dependency injections */
        services.AddScoped<ET.Application.Services.UserService, UserServiceImpl>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }

    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("SmtpSettings").Get<SmtpSettings>());
    }
}

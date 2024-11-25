﻿using DevFreela.Application.Services.Projects;
using DevFreela.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
           services.AddScoped<IProjectService, ProjectService>();
            return services;
        }
    }
}

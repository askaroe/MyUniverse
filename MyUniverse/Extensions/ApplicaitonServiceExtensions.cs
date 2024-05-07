﻿using MyUniverse.Services.Student;

namespace MyUniverse.Extensions
{
    public static class ApplicaitonServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config) 
        {
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
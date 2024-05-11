
using Microsoft.EntityFrameworkCore;
using MyUniverse.Data;
using MyUniverse.Extensions;
using System.Reflection;

namespace MyUniverse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Universe API", Version = "v1" });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

                Console.WriteLine($"{Assembly.GetExecutingAssembly().GetName().Name}");
            });

            builder.Services.AddDbContext<ApplicationContext>(options => {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Universe API");
                });
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}

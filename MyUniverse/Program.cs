
using Microsoft.EntityFrameworkCore;
using MyUniverse.Data;

namespace MyUniverse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationContext>(options => {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}

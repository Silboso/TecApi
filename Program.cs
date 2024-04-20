
using Microsoft.EntityFrameworkCore;
using TecApi.Context;

namespace TecApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            string NeonConexion = "Host=ep-patient-night-a5q7x2zc-pooler.us-east-2.aws.neon.tech;Port=5432;" +
                                  "Username=neondb_owner;Password=wPMuitEo53IS;Database=neondb;sslmode=require";

            builder.Services.AddDbContext<TecApiContext>(options =>
            {
                options.UseNpgsql(NeonConexion);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}

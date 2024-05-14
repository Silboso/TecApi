using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Controllers;

namespace TecApi
{
    public class Program
    {
        private const string API_KEY = "AIzaSyA60ILDVmASWsNK8HNAfGwcVhZiE4UXNxg";
        
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

            // Firebase Configuration
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("FirebaseConfig.json")
            });

            builder.Services.AddHttpClient<AuthController>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["BaseUrl"]!);
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
            app.Run("http://*:5104");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RoomService.Models;
using RoomService.Repositories;
using RoomService.Services;
using RoomService.Interface;

namespace RoomService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register RoomDbContext with Dependency Injection container
            builder.Services.AddDbContext<RoomDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Read connection string from appsettings.json

            // Register RoomRepository and RoomService
            builder.Services.AddScoped<IRoom, RoomRepository>(); // Register RoomRepository to IRoomRepository interface
            builder.Services.AddScoped<RoomServiceFile>();           // Register RoomService

            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Map controllers (ensures API endpoints are available)
            app.MapControllers();

            app.Run();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PaymentService.Interface;
using PaymentService.Models;
using PaymentService.Repositories;

namespace PaymentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Registering DbContext
            builder.Services.AddDbContext<PaymentDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registering repositories
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IBillRepository, BillRepository>();

            // Add controllers
            builder.Services.AddControllers();

            // Swagger/OpenAPI setup
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

            // Map controllers to the pipeline
            app.MapControllers();

            app.Run();
        }
    }
}

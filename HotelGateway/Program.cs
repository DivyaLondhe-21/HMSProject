using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HotelGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            
            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            /*builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel API Gateway", Version = "v1" });
            });*/
            //builder.Services.AddOcelot();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Gateway",
                    Version = "v1"
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            var app = builder.Build();

            app.UseCors("AllowAllOrigins");
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.


            app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway v1");
                    c.SwaggerEndpoint("/swagger/authservice.json", "AuthService API");


                    c.EnableDeepLinking();
                    c.DisplayOperationId();
                });
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseOcelot();

            app.MapControllers();

            app.Run();
        }
    }
}

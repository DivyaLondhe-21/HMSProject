
namespace EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewOnly", policy =>
                    policy.RequireRole("Admin", "Manager", "Receptionist"));

                options.AddPolicy("ManageStaff", policy =>
                    policy.RequireRole("Admin", "Manager"));

                options.AddPolicy("ManageDepartments", policy =>
                    policy.RequireRole("Admin"));
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

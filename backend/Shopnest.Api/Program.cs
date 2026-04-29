
using Microsoft.EntityFrameworkCore;
using ShopNest.Core.Interface;
using ShopNest.Infrastructure.Data;
using ShopNest.Infrastructure.Repositories;


namespace Shopnest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //database connection string is stored in appsettings.json, and we use it here to connect to SQL Server
            builder.Services.AddDbContext<ShopnestDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add controllers to the service collection, which allows us to use attribute routing and model binding in our API controllers
            builder.Services.AddControllers();

            //Repository pattern is used to abstract the data access layer, and we register the repository here for dependency injection
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
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
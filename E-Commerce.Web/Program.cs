using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            var app = builder.Build();

            #region Data Seeding
            try
            {
                using var Scope = app.Services.CreateScope();
                var objectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                objectOfDataSeeding.DataSeedAsync();
            }
            catch (Exception)
            {

                throw;
            }
            #endregion

            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseHttpsRedirection();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
} 

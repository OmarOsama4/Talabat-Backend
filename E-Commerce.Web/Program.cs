using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories; 
using Service;
using ServiceAbstraction;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            #endregion

            var app = builder.Build();

            #region Data Seeding
            await app.SeedDataBaseAsync();
            #endregion

            #region Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
} 

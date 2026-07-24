using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Web.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            });

            return Services;
        }

        public static IServiceCollection AddJWTService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWTOptions")["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JWTOptions")["Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTOptions")["SecretKey"]!))
                }; 
            });
            return Services;
        }
    }
}

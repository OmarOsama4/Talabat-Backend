using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System.Text.Json;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        StoreIdentityDbContext storeIdentityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync() 
        {
            try
            {
                var PendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigration.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    { 
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);
                    }
                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    using var DeliveryMethodData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        Email = "mohamed@gmail.com",
                        DisplayName = "Mohamed Khaled",
                        UserName = "MohamedKhaled",
                        PhoneNumber = "01000000000",
                    };
                    var User02 = new ApplicationUser
                    {
                        Email = "salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        UserName = "SalmaMohamed",
                        PhoneNumber = "01000000001",
                    };

                    await userManager.CreateAsync(User01, "P@ssw0rd");
                    await userManager.CreateAsync(User02, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await userManager.AddToRoleAsync(User02, "Admin");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

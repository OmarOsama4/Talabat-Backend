using DomainLayer.Contracts;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var objectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectOfDataSeeding.DataSeedAsync();
        }
    }
}

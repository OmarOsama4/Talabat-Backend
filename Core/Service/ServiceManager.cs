using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        IMapper mapper,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository,mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration, mapper));
        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(mapper,basketRepository,unitOfWork));
        private readonly Lazy<IPaymentService> _LazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(configuration,basketRepository,unitOfWork,mapper));


        public IProductService ProductService => _LazyproductService.Value;
        public IBasketService BasketService => _LazyBasketService.Value;
        public IAuthenticationService AuthenticationService =>_LazyAuthenticationService.Value;
        public IOrderService OrderService => _LazyOrderService.Value;
        public IPaymentService PaymentService => _LazyPaymentService.Value;
    }
}

using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _LazyproductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository,mapper));
        public IBasketService BasketService => _LazyBasketService.Value;
    }
}

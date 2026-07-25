using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using Service.Specifications.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTO;
using Shared.DataTransferObjects.OrderModuleDTO;

namespace Service
{
    public class OrderService(IMapper mapper,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string email)
        {
            var Basket = await basketRepository.GetBasketAsync(orderDTO.BasketId) ?? throw new BasketNotFoundException(orderDTO.BasketId);
            ArgumentNullException.ThrowIfNullOrEmpty(Basket.paymentIntentId);
            var OrderRepo = unitOfWork.GetRepository<Order, Guid>();

            var OrderSpec = new OrderWithPaymentIntentIdSpecification(Basket.paymentIntentId);
            var ExistingOrder = await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExistingOrder is not null) OrderRepo.Remove(ExistingOrder);
            var OrderAddress = mapper.Map<AddressDTO, OrderAddress>(orderDTO.Address);
            List<OrderItem> orderItems = [];
            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            foreach(var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem
                {
                    Product = new ProductItemOrder() { ProductId = Product.Id, ProductName = Product.Name, PictureUrl = Product.PictureUrl },
                    Price = Product.Price,
                    Quantity = item.Quantity
                }; 
                orderItems.Add(orderItem);
            }

            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDTO.DeliveryMethodId); 

            var SubTotal = orderItems.Sum(I => I.Quantity *  I.Price);

            var Order = new Order(email, OrderAddress, DeliveryMethod, orderItems, SubTotal, Basket.paymentIntentId);

            await OrderRepo.AddAsync(Order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, OrderToReturnDTO>(Order);
        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email)
        {
            var spec = new OrderSpecifications(Email);
            var Orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(Orders);
        }

        public async Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTO>>(DeliveryMethod);
        }

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id)
        {
            var spec = new OrderSpecifications(Id);
            var Order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            return mapper.Map<Order, OrderToReturnDTO>(Order);
        }
    }
} 

using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
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
            var OrderAddress = mapper.Map<AddressDTO, OrderAddress>(orderDTO.Address);
            var Basket = await basketRepository.GetBasketAsync(orderDTO.BasketId) ?? throw new BasketNotFoundException(orderDTO.BasketId);
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

            var Order = new Order(email, OrderAddress, DeliveryMethod, orderItems, SubTotal);

            await unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, OrderToReturnDTO>(Order);
        }
    }
}

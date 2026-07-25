using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDTO;
using Stripe;
using Product = DomainLayer.Models.ProductModule.Product;

namespace Service
{
    public class PaymentService(IConfiguration configuration,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var Basket = await basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);
            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);

            Basket.shippingPrice = DeliveryMethod.Price;

            var BasketAmount = (long) (Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Price) * 100;

            var PaymentService = new PaymentIntentService();
            if (Basket.paymentIntentId is null)
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD", 
                    PaymentMethodTypes = ["cards"]
                };
                var PaymentIntent =  await PaymentService.CreateAsync(Options);
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions() {Amount = BasketAmount};
                await PaymentService.UpdateAsync(Basket.paymentIntentId, Options);
            }

            await basketRepository.CreateOrUpdateBasketAsync(Basket);
            return mapper.Map<BasketDTO>(Basket);
        }
    }
}

namespace Shared.DataTransferObjects.BasketModuleDTO
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTO> Items { get; set; } = [];
        public string? clientSecret { get; set; }
        public string? paymentIntentId { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal? shippingPrice { get; set; }
    }
}

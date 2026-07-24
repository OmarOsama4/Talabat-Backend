namespace DomainLayer.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethods DeliveryMethods { get; set; } = default!;
        public int DeliveryMethodsId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        
        public decimal GetTotal() => SubTotal + DeliveryMethods.Price;
    }
}

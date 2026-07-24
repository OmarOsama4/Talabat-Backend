using DomainLayer.Models.OrderModule;
using System.Linq.Expressions;

namespace Service.Specifications.OrderModuleSpecifications
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string Email) : base(o => o.UserEmail == Email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}

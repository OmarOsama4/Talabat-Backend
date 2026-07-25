using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class OrderWithPaymentIntentIdSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentIdSpecification(string PaymentIntentId) : base(o=> o.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}

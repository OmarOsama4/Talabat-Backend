using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQuertyParams quertyParams)
            : base(p => (!quertyParams.BrandId.HasValue || p.BrandId == quertyParams.BrandId)
                  && (!quertyParams.TypeId.HasValue || p.TypeId == quertyParams.TypeId)
                  && (string.IsNullOrEmpty(quertyParams.SearchValue) || p.Name.ToLower().Contains(quertyParams.SearchValue.ToLower())))
        {
        }
    }
}

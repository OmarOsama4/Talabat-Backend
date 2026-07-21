using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products with Brand and Type
        public ProductWithBrandAndTypeSpecifications(ProductQuertyParams quertyParams)
            : base(p => (!quertyParams.BrandId.HasValue || p.BrandId == quertyParams.BrandId)
                  && (!quertyParams.TypeId.HasValue || p.TypeId == quertyParams.TypeId)
                  && (string.IsNullOrEmpty(quertyParams.SearchValue) || p.Name.ToLower().Contains(quertyParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (quertyParams.SortingOptions)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                default:
                    break;
            }
        }

        //Get Product by Id with Brand and Type
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

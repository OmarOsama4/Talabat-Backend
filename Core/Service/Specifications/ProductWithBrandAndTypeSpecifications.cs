using DomainLayer.Models;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products with Brand and Type
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        //Get Product by Id with Brand and Type
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

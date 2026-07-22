using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,
        IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDTO = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTO>>(Brands);
            return BrandsDTO;
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync() 
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDTO = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
            return TypesDTO;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            return mapper.Map<Product, ProductDTO>(Product);
        }

        public async Task<PaginatedResult<ProductDTO>> GetProductsAsync(ProductQuertyParams quertyParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var spec = new ProductWithBrandAndTypeSpecifications(quertyParams);
            var Products = await Repo.GetAllAsync(spec);
            var ProductsDTO = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
            var ProductCount = Products.Count();
            var countSpec = new ProductCountSpecification(quertyParams);
            var TotalCount = await Repo.CountAsync(countSpec);
            return new PaginatedResult<ProductDTO>(ProductCount, quertyParams.PageIndex, TotalCount, ProductsDTO); 
        }
    }
}

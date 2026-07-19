using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
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
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return mapper.Map<Product, ProductDTO>(Product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var ProductsDTO = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
            return ProductsDTO;
        }
    }
}

namespace DomainLayer.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int BrandId { get; set; } //FK
        public int TypeId { get; set; } //FK 


        #region Navigation Property
        public ProductBrand ProductBrand { get; set; }
        public ProductType ProductType { get; set; }
        #endregion
    }
}

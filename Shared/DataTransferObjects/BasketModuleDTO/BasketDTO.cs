namespace Shared.DataTransferObjects.BasketModuleDTO
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTO> Items { get; set; } = [];
    }
}

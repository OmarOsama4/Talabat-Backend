namespace DomainLayer.Models
{
    public class BaseEntity<Tkey>
    {
        public Tkey Id { get; set; } //PK
    }
}

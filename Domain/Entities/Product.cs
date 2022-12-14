using Domain.Interfaces;

namespace Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
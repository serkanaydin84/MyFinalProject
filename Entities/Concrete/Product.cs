using Core.Entities;

namespace Entities.Concrete
{
    // public yapmamızın nedeni bu class a diğer katmanlar da ulaşa bilsin demek
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

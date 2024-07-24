namespace PMS_Entity
{
    public class Products : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Quantity { get; set; }
        public string? ProductType { get; set; }
        public string? sku { get; set; }
    }

}

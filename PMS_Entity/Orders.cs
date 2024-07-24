using System.ComponentModel.DataAnnotations.Schema;

namespace PMS_Entity
{
    public class Orders : BaseEntity
    {
        public string OrderNumber { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public decimal OrderQty { get; set; } = 0;
        public DateTime OrderDate { get; set; }= DateTime.UtcNow;
        public decimal TotalPrice { get; set; } = 0;
        public Products product { get; set; }
    }
}

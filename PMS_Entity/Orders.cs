using System.ComponentModel.DataAnnotations.Schema;

namespace PMS_Entity
{
    public class Orders : BaseEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }  
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public decimal TotalAmount { get; set; } = 0;
        public Products product { get; set; }
    }
}

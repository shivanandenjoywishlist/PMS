using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_Entity
{
    public class ProductInventory:BaseEntity
    {
        public decimal CorrentStock { get; set; } = 0;

        [ForeignKey("product")]
        public int ProductId {get; set; }
        public Products product { get; set; }

    }
}

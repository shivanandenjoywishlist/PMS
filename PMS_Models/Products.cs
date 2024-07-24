using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_Models
{
    public class Products : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public string? ProductType { get; set; } = "Amazone";
    }
}

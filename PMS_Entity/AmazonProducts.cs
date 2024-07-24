using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_Entity
{
    public class AmazonProducts:BaseEntity
    {
        public string ProductName { get; set; }
        public string sku { get; set; } 
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

       
    }


    public class FlipKartProducts : BaseEntity
    {
        public string ProductName { get; set; }
        public string sku { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

    }
}

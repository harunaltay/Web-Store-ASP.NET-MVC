using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebStore.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Category Category { get; set; }
    }
}
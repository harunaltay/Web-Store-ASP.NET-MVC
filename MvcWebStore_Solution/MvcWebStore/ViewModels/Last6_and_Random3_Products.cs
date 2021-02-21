using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcWebStore.Models;

namespace MvcWebStore.ViewModels
{
    public class Last6_and_Random3_Products
    {
        public List<Product> Last6Products { get; set; }
        public List<Product> Random3Products { get; set; }

    }
}
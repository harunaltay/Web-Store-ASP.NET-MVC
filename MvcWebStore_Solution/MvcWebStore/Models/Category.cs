﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
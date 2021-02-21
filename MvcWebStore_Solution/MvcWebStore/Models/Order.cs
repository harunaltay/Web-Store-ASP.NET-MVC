using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebStore.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        [ScaffoldColumn(false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "First name is required!")]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required!")]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required!")]
        [StringLength(50)]
        public string State { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [DisplayName("Postal Code")]
        [StringLength(100)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [StringLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required!")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email address is required!")]
        [DisplayName("Email address")]
        [StringLength(50)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
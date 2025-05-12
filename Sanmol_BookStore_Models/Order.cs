using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmol_BookStore_Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookTitle { get; set; }
        public string OrderStatus { get; set; }
    }
}

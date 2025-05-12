using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sanmol_BookStore_Models;

namespace Sanmol_BookStore.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            // Sample order data (this could be from a database in a real application)
            var orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerName = "Prajakta Mali", BookTitle = "Shyamchi Aai", OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 19.99m, OrderStatus = "Completed" },
            new Order { OrderId = 2, CustomerName = "Sachin Tendulakar", BookTitle = "1984", OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 14.99m, OrderStatus = "Pending" },
            new Order { OrderId = 3, CustomerName = "Vedant Bodke", BookTitle = "The Secret", OrderDate = DateTime.Now.AddDays(-3), TotalAmount = 12.99m, OrderStatus = "Shipped" }
        };

            return View(orders);
        }
    }
}

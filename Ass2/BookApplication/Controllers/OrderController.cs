using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Data;

namespace BookApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Book)
                .Include(o => o.Customer)
                .Include(o => o.Seller)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.Book)
                .Include(o => o.Customer)
                .Include(o => o.Seller)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();
            return View(order);
        }
    }
}

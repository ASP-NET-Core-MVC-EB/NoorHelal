using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Data;

namespace BookApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BookStoreContext _context;

        public CustomerController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers
                .Include(c => c.Orders)
                .ToList();

            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.Book)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();
            return View(customer);
        }
    }
}

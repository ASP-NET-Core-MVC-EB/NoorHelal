using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Data;

namespace BookApplication.Controllers
{
    public class SellerController : Controller
    {
        private readonly BookStoreContext _context;

        public SellerController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sellers = _context.Sellers
                .Include(s => s.Books)
                .Include(s => s.Orders)
                .ToList();

            return View(sellers);
        }

        public IActionResult Details(int id)
        {
            var seller = _context.Sellers
                .Include(s => s.Books)
                .Include(s => s.Orders)
                .ThenInclude(o => o.Customer)
                .FirstOrDefault(s => s.Id == id);

            if (seller == null) return NotFound();
            return View(seller);
        }
    }
}

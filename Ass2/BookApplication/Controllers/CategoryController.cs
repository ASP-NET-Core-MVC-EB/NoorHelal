using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Data;

namespace BookApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookStoreContext _context;

        public CategoryController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories
                .Include(c => c.Books)
                .ToList();

            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _context.Categories
                .Include(c => c.Books)
                .ThenInclude(b => b.Author)
                .FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();
            return View(category);
        }
    }
}

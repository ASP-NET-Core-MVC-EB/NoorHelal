using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Data;

namespace BookApplication.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookStoreContext _context;

        public AuthorController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors
                .Include(a => a.Books)
                .ToList();

            return View(authors);
        }

        public IActionResult Details(int id)
        {
            var author = _context.Authors
                .Include(a => a.Books)
                .ThenInclude(b => b.Category)
                .FirstOrDefault(a => a.Id == id);

            if (author == null) return NotFound();
            return View(author);
        }
    }
}

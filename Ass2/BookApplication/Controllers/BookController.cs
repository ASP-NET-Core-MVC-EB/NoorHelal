using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Data;

namespace BookApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly BookStoreContext _context;

        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Category).Include(b => b.Seller).ToList();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Seller)
                .FirstOrDefault(b => b.Id == id);

            if (book == null) return NotFound();
            return View(book);
        }
    }
}

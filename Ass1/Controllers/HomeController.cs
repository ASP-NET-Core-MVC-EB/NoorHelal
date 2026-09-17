using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Data;
using RestaurantApplication.Models;
using RestaurantApplication.ViewModel;
using System.Diagnostics;

namespace RestaurantApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RestaurantDbContext _context;

        public HomeController(ILogger<HomeController> logger,RestaurantDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(SearchVM search)
        {
            var query = _context.Categories.Include(a => a.Foods)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.SearchString))
            {
                
                query = query.Where(a => a.Foods.Any(x => x.Name.Contains(search.SearchString.Trim())));
            }

            if (search.CategoryId != null)
            {
                query = query.Where(a => a.Id == search.CategoryId);

            }

            search.Categories = await query.ToListAsync();
            var categories = await _context.Categories.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();

            search.CategoryList = categories;


            return View(search);
        }
        public IActionResult Book()
        {
            return View();
        }
       
        public IActionResult Menu()
        {
            var categories = _context.Categories.Include(a => a.Foods)
                .AsNoTracking().ToList();
            return View(categories);
        }
        public IActionResult About()
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

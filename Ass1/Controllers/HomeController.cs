using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Data;
using RestaurantApplication.Models;

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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Book()
        {
            return View();
        }
       
        public IActionResult Menu()
        {
            var categories = _context.Categories.Include(a => a.Foods).ToList();

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

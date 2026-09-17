using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Data;
using RestaurantApplication.Models;
using RestaurantApplication.Services;
using RestaurantApplication.ViewModel;

namespace RestaurantApplication.Controllers
{
    public class FoodController : Controller
    {
        private readonly RestaurantDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _IImageService;

        public FoodController(RestaurantDbContext context, IWebHostEnvironment webHostEnvironment, IImageService IImageService)
        {
            _context = context;
            _IImageService = IImageService;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            
            var foods = await _context.Foods.Include(a=> a.Category).AsNoTracking().OrderBy(a => a.Name).ToListAsync();
            return View(foods);

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();


            var foodVm = new FoodVM
            {
                CategoryList = categories
            };
            return View(foodVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodVM foodVM)
        {
            var categories = await _context.Categories.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();


            foodVM.CategoryList = categories;

            if (!ModelState.IsValid)
            {
                return View(foodVM);
            }

            if (await _context.Foods.AnyAsync(a => a.Name.ToLower() == foodVM.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Food Name Already Exists!");
            }
            try
            {
                if(foodVM.ImageFile != null)
                {
                  string path=   await _IImageService.UploadAndResizeImageAsync(foodVM.ImageFile, "food");
                    foodVM.ImageUrl = path;
                }
                
                _context.Foods.Add(foodVM);
                await _context.SaveChangesAsync();
                TempData["SuccessMsg"] = $"{foodVM.Name} Food Saved Successfully !";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An Error Occurred During Save Item, Try Later");
            }
            return View(foodVM);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id,string? from)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            var foodVM = new FoodVM
            {
                Id = food.Id,
                Name = food.Name,
                Price = food.Price,
                Description = food.Description,
                ImageUrl = food.ImageUrl,
                CategoryId = food.CategoryId,
                CategoryList = await _context.Categories.Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToListAsync()
            };
            ViewBag.source = from;
            return View(foodVM);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.Include(a=> a.Category).AsNoTracking().FirstOrDefaultAsync(a=> a.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FoodVM foodVM,string? source)
        {
            var categories = await _context.Categories.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();


            foodVM.CategoryList = categories;

            if (!ModelState.IsValid)
            {
                return View(foodVM);
            }

            
            try
            {
                if (foodVM.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(foodVM.ImageUrl))
                    {
                        _IImageService.DeleteImage(foodVM.ImageUrl);
                    }
                    string path = await _IImageService.UploadAndResizeImageAsync(foodVM.ImageFile, "food");
                    foodVM.ImageUrl = path;
                }

                _context.Foods.Update(foodVM);
                await _context.SaveChangesAsync();
                TempData["SuccessMsg"] = $"{foodVM.Name} Food Updated Successfully !";
                if(source == "details")
                {
                    return RedirectToAction(nameof(Details), new { id = foodVM.Id });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An Error Occurred During Save Item, Try Later");
            }
            return View(foodVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return Json(new { success = false, message = "The Item Not Found!" });
            }
            try
            {
                if (!string.IsNullOrEmpty(food.ImageUrl))
                {
                    _IImageService.DeleteImage(food.ImageUrl);
                }
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
                TempData["SuccessMsg"] = $"{food.Name} food Deleted Successfully !";
                return Json(new { success = true, foodId = id, message = "food Deleted Successfully!" });


            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An Error Occurred During Delete Item, Try Later" });
            }


            //return RedirectToAction(nameof(Index));


        }
    }
}

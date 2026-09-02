using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Data;
using RestaurantApplication.Models;
using RestaurantApplication.ViewModel;

namespace RestaurantApplication.Controllers
{
    public class FoodController : Controller
    {
        private readonly RestaurantDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodController(RestaurantDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var foods = await _context.Foods.Include(a=> a.Category).OrderBy(a => a.Name).ToListAsync();
            return View(foods);
        }
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
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(foodVM.ImageFile.FileName);
                    string path = Path.Combine(wwwRootPath,@"images\food");

                    using(var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await foodVM.ImageFile.CopyToAsync(fileStream);
                    }
                    foodVM.ImageUrl = @"images\food\" + fileName;
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (await _context.Categories.AnyAsync(a => a.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Category Name Already Exists!");
            }
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMsg"] = $"{category.Name} Category Updated Successfully !";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An Error Occurred During Save Item, Try Later");
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return Json(new { success = false, message = "The Item Not Found!" });
            }
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMsg"] = $"{category.Name} Category Deleted Successfully !";
                return Json(new { success = true, categoryId = id, message = "Category Deleted Successfully!" });


            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An Error Occurred During Delete Item, Try Later" });
            }


            //return RedirectToAction(nameof(Index));


        }
    }
}

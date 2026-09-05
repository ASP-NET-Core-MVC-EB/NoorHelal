using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Data;
using RestaurantApplication.Models;

namespace RestaurantApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly RestaurantDbContext _context;

        public CategoryController(RestaurantDbContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.OrderBy(a=>a.DisplayOrder).ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if(await _context.Categories.AnyAsync(a=> a.Name.ToLower() == category.Name.ToLower())){
                ModelState.AddModelError("Name", "Category Name Already Exists!");
            }
            try
            {
                _context.Categories.Add(category);
               await _context.SaveChangesAsync();
               TempData["SuccessMsg"] = $"{category.Name} Category Saved Successfully !";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An Error Occurred During Save Item, Try Later");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }   
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Category category)
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
                return Json(new { success = false, message ="The Item Not Found!"});
            }
            try
            {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["SuccessMsg"] = $"{category.Name} Category Deleted Successfully !";
                return Json(new { success = true, categoryId= id, message = "Category Deleted Successfully!" });


            }catch (Exception ex)
            {
                return Json(new { success = false, message = "An Error Occurred During Delete Item, Try Later" });
            }


            //return RedirectToAction(nameof(Index));


        }
    }
}

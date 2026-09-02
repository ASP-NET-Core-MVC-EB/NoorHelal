using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApplication.Models;

namespace RestaurantApplication.ViewModel
{
    public class FoodVM : Food
    {
       // public Food food { get; set; } = new Food();

        public IFormFile? ImageFile { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

    }
}

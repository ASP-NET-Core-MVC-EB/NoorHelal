using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApplication.Models;

namespace RestaurantApplication.ViewModel
{
    public class SearchVM
    {
        public string? SearchString { get; set; }
        public int? CategoryId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

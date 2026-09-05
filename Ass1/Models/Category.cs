using System.ComponentModel.DataAnnotations;

namespace RestaurantApplication.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 100 characters.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        

        // Navigation
        public ICollection<Food> Foods { get; set; } = new List<Food>();

    }
}

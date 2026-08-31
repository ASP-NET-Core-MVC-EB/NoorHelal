using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApplication.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Food name is required.")]
        [StringLength(100, ErrorMessage = "Food name cannot exceed 100 characters.")]
        [Display(Name = "Food Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }


        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]

        //99.99
        [Precision(18, 2)]
        public decimal Price { get; set; }

        //Navigation
        public int CategoryId { get; set; }

        public Category Category { get; set; } 
      
    }
}

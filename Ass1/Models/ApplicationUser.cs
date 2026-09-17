using Microsoft.AspNetCore.Identity;

namespace RestaurantApplication.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; }
    }
}

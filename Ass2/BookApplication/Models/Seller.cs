namespace BookApplication.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

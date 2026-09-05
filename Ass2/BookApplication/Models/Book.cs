namespace BookApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }
        public Seller Seller { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}

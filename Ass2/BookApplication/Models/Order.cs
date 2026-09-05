namespace BookApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // Foreign Keys
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }

        // Navigation
        public Book Book { get; set; }
        public Customer Customer { get; set; }
        public Seller Seller { get; set; }

       

    }
}

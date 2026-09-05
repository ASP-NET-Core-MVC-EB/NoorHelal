using BookApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace RazorDemo.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Order -> Book
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Book)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> Seller
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Seller)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "John Smith", Biography = "Expert in C# programming" },
                new Author { Id = 2, Name = "Jane Doe", Biography = "Specialist in ASP.NET Core" },
                new Author { Id = 3, Name = "Michael Brown", Biography = "Database and ORM expert" }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Programming", Description = "All programming books" },
                new Category { Id = 2, Name = "Web Development", Description = "Books about building websites" },
                new Category { Id = 3, Name = "Databases", Description = "Books about databases" }
            );

            // Seed Sellers
            modelBuilder.Entity<Seller>().HasData(
                new Seller { Id = 1, Name = "Book World", ContactInfo = "bookworld@email.com" },
                new Seller { Id = 2, Name = "Tech Reads", ContactInfo = "techreads@email.com" }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FullName = "Alice Johnson", Email = "alice@email.com" },
                new Customer { Id = 2, FullName = "Bob Williams", Email = "bob@email.com" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "C# Basics", Description = "Learn C# fundamentals", AuthorId = 1, CategoryId = 1, SellerId = 1 },
                new Book { Id = 2, Title = "ASP.NET Core MVC", Description = "Master MVC framework", AuthorId = 2, CategoryId = 2, SellerId = 2 }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, BookId = 1, CustomerId = 1, SellerId = 1, OrderDate = DateTime.Now.AddDays(-3) },
                new Order { Id = 2, BookId = 2, CustomerId = 2, SellerId = 2, OrderDate = DateTime.Now.AddDays(-1) }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace BetApi.Model
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
             : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("AspNetUsers");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<Order>().ToTable("Order");
        }
    }
}

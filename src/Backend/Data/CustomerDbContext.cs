using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        // Why doesn't this work for seeding the database?
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // Seed the database.
        //     int customerId = 1;
        //     foreach (var name in new[] { "John", "Matt", "Omair", "Dan", "Radka", "Andrew", "Tom" })
        //     {
        //         modelBuilder.Entity<Customer>().HasData(
        //             new Customer { Id = customerId++, Name = name });
        //     }
        // }
    }
}
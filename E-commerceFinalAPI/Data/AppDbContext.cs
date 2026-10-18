using E_commerceFinalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerceFinalAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; } 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x=>x.Email).IsUnique();

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<User>()
                .HasMany(c=>c.Orders)
                .WithOne(c=>c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);

            modelBuilder.Entity<Product>()
               .HasMany(c => c.orderItems)
               .WithOne(c => c.Product)
               .HasForeignKey(c => c.ProductId);


            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    Id=1,
                    FullName="Nada Safaa",
                    Email="nada@gmail.com",
                    Password="01120099839",
                    Role="Customer"
                },
                 new User
                 {
                     Id = 2,
                     FullName = "Toka Reda",
                     Email = "Toka@gmail.com",
                     Password = "01120099839",
                     Role = "Admin"
                 }
                );

            modelBuilder.Entity<Category>()
                .HasData(
                new Category
                {
                    Id=1,
                    Name="Electrons",
                    Description="qwertyuiopsdfghjkl"
                },
                 new Category
                 {
                     Id = 2,
                     Name = "Clothes",
                     Description = "qwertyuiopsdfghjkl"
                 }

                );

            modelBuilder.Entity<Order>()
                .HasData(
                new Order
                {
                    Id=1,
                    OrderDate= new DateTime(2026-01-23),
                    status="Completed",
                    TotalAmount=200,
                    UserId=1

                },
                new Order
                {
                    Id = 2,
                    OrderDate = new DateTime(2025 - 03 - 20),
                    status = "Cancelled",
                    TotalAmount = 300,
                    UserId = 1

                }
                );

            modelBuilder.Entity<Product>()
                .HasData(
                new Product
                {
                    Id=1,
                    Name="TV",
                    Description="wertuio",
                    StockQuantity=9,
                    CategoryId=1,
                    Price=200
                },
                 new Product
                 {
                     Id = 2,
                     Name = "Pants",
                     Description = "ppppppppp",
                     StockQuantity = 7,
                     CategoryId = 2,
                     Price = 250
                 }

                );

            modelBuilder.Entity<OrderItem>()
                .HasData(
                new OrderItem
                {
                    Id=1,
                    OrderId=1,
                    ProductId=1,
                    Quantity=3,
                    UnitPrice=300
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 2,
                    ProductId = 2,
                    Quantity = 3,
                    UnitPrice = 100
                }

                );


        }
    }
}

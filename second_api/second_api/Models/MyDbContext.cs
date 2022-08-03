using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e => {
                e.ToTable("Product");
                e.HasKey(p => p.Id);
                e.Property(p => p.Created).HasDefaultValueSql("getutcdate()");
                e.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.HasKey(c => c.Id);
                e.Property(c => c.Created).HasDefaultValueSql("getutcdate()");
                e.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ProductCategory>(e => {
                e.ToTable("ProductCategory");
                e.HasKey(pc => new {pc.ProId, pc.CateId });

                e.HasOne(pc => pc.product)
                         .WithMany(pc => pc.productCategories)
                         .HasForeignKey(pc => pc.ProId)
                         .HasConstraintName("FK_PRODUCT");

                e.HasOne(pc => pc.category)
                        .WithMany(pc => pc.productCategories)
                        .HasForeignKey(pc => pc.CateId)
                        .HasConstraintName("FK_CATEGORY");
            });
        }

        public DbSet<Product> products { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<ProductCategory> productCategories { get; set; }
     }
}

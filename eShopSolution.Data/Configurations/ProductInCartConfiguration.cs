using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Configurations
{
    public class ProductInCartConfiguration : IEntityTypeConfiguration<ProductInCart>
    {
        public void Configure(EntityTypeBuilder<ProductInCart> builder)
        {
            builder.HasKey(t => new { t.CartId, t.ProductId });

            builder.ToTable("ProductInCart");

            builder.HasOne(t => t.Product).WithMany(pc => pc.ProductInCarts)
                .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(t => t.Cart).WithMany(pc => pc.ProductInCarts)
              .HasForeignKey(pc => pc.CartId);
        }
    }
}

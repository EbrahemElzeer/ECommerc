using ECommerce.Domin.Model.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           builder.Property(x=>x.Price)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(x => x.Product, pi =>
            {
                pi.Property(x => x.ProductId);
                pi.Property(x => x.ProductName).HasMaxLength(100);
                pi.Property(x => x.PictureUrl).HasMaxLength(200);
            });
        }
    }
}

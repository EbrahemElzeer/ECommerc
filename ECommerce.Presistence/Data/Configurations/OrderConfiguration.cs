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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x=>x.Subtotal)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(x => x.Address, oe =>
            {
                oe.Property(x => x.FirstName).HasMaxLength(50);
                oe.Property(x => x.LastName).HasMaxLength(50);
                oe.Property(x => x.City).HasMaxLength(50);
                oe.Property(x => x.Street).HasMaxLength(50);
                oe.Property(x => x.Country).HasMaxLength(50);
            });
        }
    }
}

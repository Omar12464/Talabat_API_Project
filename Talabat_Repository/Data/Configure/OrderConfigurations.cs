using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Order_Aggregate;

namespace Talabat_Repository.Data.Configure
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(InputSystem => InputSystem.ToString(),
                InputSystem => (OrderStatus)Enum.Parse(typeof(OrderStatus),InputSystem));
            builder.OwnsOne(o => o.ShippingAddress, shippingadd => shippingadd.WithOwner());
            //builder.HasOne(o => o.deliveryMethod)
            //    .WithOne();
            //builder.HasIndex(o => o.deliveryMethod).IsUnique();
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");

        }
    }
}

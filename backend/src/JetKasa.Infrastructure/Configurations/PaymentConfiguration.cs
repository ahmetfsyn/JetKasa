using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JetKasa.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Amount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();
            builder.Property(p => p.Status)
                   .HasMaxLength(50)
                   .HasDefaultValue("Success");
            builder.Property(p => p.CardLast4)
                   .HasMaxLength(4);
        }
    }
}
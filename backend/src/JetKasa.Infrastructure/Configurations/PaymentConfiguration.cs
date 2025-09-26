using JetKasa.Domain.Carts;
using JetKasa.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JetKasa.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // Cart ile bire bir ilişki 
        builder.HasOne(p => p.Cart)
               .WithOne(c => c.Payment)
               .HasForeignKey<Payment>(p => p.CartId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Method)
               .HasConversion<string>();
    }
}

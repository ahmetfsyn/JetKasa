using JetKasa.Domain.Carts;
using JetKasa.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JetKasa.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(c => c.Status)
                   .HasConversion<string>();

            // CartItem ile ilişki
            builder.HasMany(c => c.CartItems)
                   .WithOne(ci => ci.Cart)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Payment ile bire bir ilişki
            builder.HasOne(c => c.Payment)
                   .WithOne(p => p.Cart)
                   .HasForeignKey<Payment>(p => p.CartId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

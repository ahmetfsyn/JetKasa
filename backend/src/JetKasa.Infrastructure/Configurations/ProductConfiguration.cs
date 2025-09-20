using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JetKasa.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();

            builder.Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();

            builder.Property(p => p.Barcode).HasMaxLength(50).IsRequired();

            builder.HasIndex(p => p.Barcode).IsUnique();

            builder.Property(p => p.Stock).HasDefaultValue(0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Abstractions;
using JetKasa.Domain.CartItems;

namespace JetKasa.Domain.Products;

public class Product : Entity
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public string Barcode { get; set; } = default!;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

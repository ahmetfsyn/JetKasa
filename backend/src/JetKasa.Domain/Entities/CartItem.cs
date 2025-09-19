using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Abstractions;
using JetKasa.Domain.Carts;
using JetKasa.Domain.Products;

namespace JetKasa.Domain.CartItems;

public class CartItem : Entity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.UtcNow;

    public Cart Cart { get; set; } = default!;
    public Product Product { get; set; } = default!;
}

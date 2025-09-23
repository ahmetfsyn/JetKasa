using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetKasa.Domain.Dtos;

public sealed class CartItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}

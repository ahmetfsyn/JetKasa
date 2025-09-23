using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.CartItems;
using JetKasa.Domain.Enums;
using JetKasa.Domain.Payments;

namespace JetKasa.Domain.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        // public CartStatus Status { get; set; } = CartStatus.Active;

        public List<CartItemDto> ItemDtos { get; set; } = new();

    }
}
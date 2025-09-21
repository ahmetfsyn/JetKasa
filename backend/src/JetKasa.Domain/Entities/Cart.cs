using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Abstractions;
using JetKasa.Domain.CartItems;
using JetKasa.Domain.Enums;
using JetKasa.Domain.Payments;

namespace JetKasa.Domain.Carts;

public class Cart : Entity
{
    public CardStatus Status { get; set; } = CardStatus.Pending;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public Payment? Payment { get; set; }
}

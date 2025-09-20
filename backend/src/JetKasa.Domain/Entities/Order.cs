using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Abstractions;
using JetKasa.Domain.Carts;
using JetKasa.Domain.Payments;
using JetKasa.Domain.Users;

namespace JetKasa.Domain.Entities;

public class Order : Entity
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Cart Cart { get; set; } = default!;
    public User User { get; set; } = default!;
    public Payment Payment { get; set; } = default!;
}

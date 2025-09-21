using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Abstractions;
using JetKasa.Domain.Carts;
using JetKasa.Domain.Enums;

namespace JetKasa.Domain.Payments;

public class Payment : Entity
{
    public Guid CartId { get; set; }
    public PaymentMethod Method { get; set; }
    public decimal Total { get; set; }
    public DateTimeOffset PaidAt { get; set; } = DateTimeOffset.UtcNow;

    public Cart Cart { get; set; } = default!;
}

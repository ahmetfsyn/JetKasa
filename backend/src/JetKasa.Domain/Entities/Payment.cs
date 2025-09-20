using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Abstractions;
using JetKasa.Domain.Entities;

namespace JetKasa.Domain.Payments;

public class Payment : Entity
{
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Success";
    public Guid TransactionId { get; set; }
    public string CardLast4 { get; set; } = default!;
    public DateTimeOffset ProcessedAt { get; set; } = DateTimeOffset.UtcNow;

    public Order? Order { get; set; }

}

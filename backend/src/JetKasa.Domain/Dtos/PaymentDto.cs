using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Enums;

namespace JetKasa.Domain.Dtos;

public sealed class PaymentDto
{
    public Guid Id { get; set; }
    public PaymentMethod Method { get; set; } = PaymentMethod.Card;
    public CartDto CartDto { get; set; } = default!;
    public decimal Total { get; set; }
    public decimal OriginalTotal { get; set; }
    public DateTimeOffset PaidAt { get; set; } = DateTimeOffset.UtcNow;


    public string UserName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
}
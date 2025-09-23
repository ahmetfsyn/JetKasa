
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Dtos;
using JetKasa.Domain.Payments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace JetKasa.Application.Queries.PaymentQuery;

public sealed record GetPaymentByCartIdQuery(Guid CartId) : IRequest<Result<PaymentDto>>;

internal sealed class GetPaymentByCartIdQueryHandler(IPaymentRepository paymentRepository) : IRequestHandler<GetPaymentByCartIdQuery, Result<PaymentDto>>
{
    public async Task<Result<PaymentDto>> Handle(GetPaymentByCartIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetAll()
         .Where(p => p.CartId == request.CartId)
         .Select(p => new PaymentDto
         {
             Id = p.Id,
             CartId = p.CartId,
             Total = p.Total,
             Method = p.Method,
             PaidAt = p.PaidAt
         })
         .FirstOrDefaultAsync(cancellationToken);

        if (payment is null)
        {
            return Result<PaymentDto>.Failure("Bu sepete ait ödeme mevcut değildir!");
        }

        return Result<PaymentDto>.Succeed(payment);
    }
}


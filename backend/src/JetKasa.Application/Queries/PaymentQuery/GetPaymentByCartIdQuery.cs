
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Dtos;
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
         .Include(c => c.Cart)
         .ThenInclude(i => i.CartItems)
         .ThenInclude(p => p.Product)
         .Where(c => c.CartId == request.CartId)
         .Select(p => new PaymentDto
         {
             Id = p.Id,
             PaidAt = p.PaidAt,
             Method = p.Method,
             CartDto = new CartDto
             {
                 Id = p.Cart.Id,
                 ItemDtos = p.Cart.CartItems.Select(i => new CartItemDto
                 {
                     Id = i.Id,
                     ProductName = i.Product.Name,
                     Quantity = i.Quantity,
                     Discount = i.Discount,
                     Price = i.Price
                 }).ToList()
             },
             Total = p.Total,
         })
         .FirstOrDefaultAsync(cancellationToken);

        if (payment is null)
        {
            return Result<PaymentDto>.Failure("Bu sepete ait ödeme mevcut değildir!");
        }

        return Result<PaymentDto>.Succeed(payment);
    }
}


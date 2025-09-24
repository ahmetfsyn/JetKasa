using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Dtos;
using JetKasa.Domain.Enums;
using JetKasa.Domain.Payments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace JetKasa.Application.Command.PaymentCommand;

public sealed record CreatePaymentByCartIdCommand(Guid CartId) : IRequest<Result<string>>;

internal sealed class CreatePaymentByCartIdCommandHandler(ICartRepository cartRepository, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, INovuService novuService) : IRequestHandler<CreatePaymentByCartIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreatePaymentByCartIdCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAllWithTracking()
       .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Product)
       .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

        if (cart is null)
            return Result<string>.Failure("Sepet bulunamadı!");

        switch (cart.Status)
        {
            case CartStatus.Completed:
                return Result<string>.Failure("Ödeme zaten gerçekleştirilmiştir!");
            case CartStatus.Cancelled:
                return Result<string>.Failure("Sepet iptal edilmiştir!");
            case CartStatus.Active:
                break;
        }

        if (!cart.CartItems.Any())
            return Result<string>.Failure("Sepette ürün mevcut değildir!");

        var cartTotal = cart.CartItems.Sum(i => i.Price * (1 - i.Discount) * i.Quantity);

        var originalTotal = cart.CartItems.Sum(i => i.Price * i.Quantity);

        var payment = new Payment
        {
            CartId = cart.Id,
            Total = cartTotal,
            Method = PaymentMethod.Card,
            PaidAt = DateTime.UtcNow
        };

        await paymentRepository.AddAsync(payment, cancellationToken);

        cart.Status = CartStatus.Completed;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var paymentDto = new PaymentDto
        {
            Id = payment.Id,
            PaidAt = payment.PaidAt,
            Method = payment.Method,
            Total = payment.Total,
            OriginalTotal = originalTotal,
            CartDto = new CartDto
            {
                Id = cart.Id,
                ItemDtos = cart.CartItems.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Discount = i.Discount
                }).ToList()
            },
        };

        await novuService.SendReceiptAsync(paymentDto);

        return Result<string>.Succeed("Ödeme başarılı ve fiş gönderildi!");
    }
}

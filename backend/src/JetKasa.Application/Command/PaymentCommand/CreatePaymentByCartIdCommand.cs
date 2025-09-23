using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Enums;
using JetKasa.Domain.Payments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace JetKasa.Application.Command.PaymentCommand;

public sealed record CreatePaymentByCartIdCommand(Guid CartId) : IRequest<Result<string>>;

internal sealed class CreatePaymentByCartIdCommandHandler(ICartRepository cartRepository, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreatePaymentByCartIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreatePaymentByCartIdCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAllWithTracking()
     .Include(c => c.CartItems)
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

        var payment = new Payment
        {
            CartId = cart.Id,
            Total = cartTotal,
            Method = PaymentMethod.Card
        };

        await paymentRepository.AddAsync(payment, cancellationToken);

        cart.Status = CartStatus.Completed;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Ödeme başarılı!");
    }
}

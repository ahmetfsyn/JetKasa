using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Enums;
using MediatR;
using TS.Result;

namespace JetKasa.Application.Command.CartCommand;

public sealed record CancelCartCommand(Guid Id) : IRequest<Result<string>>;
internal sealed class CancelCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork) : IRequestHandler<CancelCartCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CancelCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByExpressionAsync(i => i.Id == request.Id, cancellationToken);

        if (cart is null)
            return Result<string>.Failure("Sepet bulunamadı!");

        if (cart.Status == CartStatus.Completed)
            return Result<string>.Failure("Ödeme yapılmış sepet iptal edilemez!");

        cart.Status = CartStatus.Cancelled;

        cart.CartItems.Clear();

        cartRepository.Update(cart);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Sepet iptal edildi!");
    }
}

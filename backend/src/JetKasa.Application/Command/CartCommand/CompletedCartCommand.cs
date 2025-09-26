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

public sealed record CompletedCartCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class CompletedCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork) : IRequestHandler<CompletedCartCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CompletedCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByExpressionAsync(i => i.Id == request.Id, cancellationToken);

        if (cart is null)
        {
            return Result<string>.Failure("Sepet bulunamadı!");
        }
        if (cart.Status == CartStatus.Cancelled)
        {
            return Result<string>.Failure("Sepet iptal edilmiş!");
        }

        cart.Status = CartStatus.Completed;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Sepet tamamlandı.");
    }
}

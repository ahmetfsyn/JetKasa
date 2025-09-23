using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Carts;
using JetKasa.Domain.Enums;
using MediatR;
using TS.Result;

namespace JetKasa.Application.Command.CartCommand;

public sealed record CreateCartCommand() : IRequest<Result<Guid>>;

internal sealed class CreateCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCartCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new Cart
        {
            Status = CartStatus.Active
        };

        await cartRepository.AddAsync(cart);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Succeed(cart.Id);
    }
}

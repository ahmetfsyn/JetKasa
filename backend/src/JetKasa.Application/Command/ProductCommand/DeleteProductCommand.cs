using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using MediatR;
using TS.Result;

namespace JetKasa.Application.Command.ProductCommand;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result<string>.Failure("Ürün bulunamadı");
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Ürün başarıyla silindi.");

    }
}

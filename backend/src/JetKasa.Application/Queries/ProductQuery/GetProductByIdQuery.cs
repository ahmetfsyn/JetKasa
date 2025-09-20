using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Products;
using MediatR;
using TS.Result;

namespace JetKasa.Application.Queries.ProductQuery;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<Product>>;

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result<Product>.Failure("Ürün bulunamadı!");
        }

        return Result<Product>.Succeed(product);
    }
}

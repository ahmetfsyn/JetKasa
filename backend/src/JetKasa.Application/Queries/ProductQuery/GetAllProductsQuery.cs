using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace JetKasa.Application.Queries.ProductQuery;

public sealed record GetAllProductsQuery() : IRequest<Result<List<Product>>>;

internal sealed class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAll().ToListAsync(cancellationToken);

        return Result<List<Product>>.Succeed(products);
    }
}

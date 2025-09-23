using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.CartItems;
using JetKasa.Domain.Carts;
using JetKasa.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace JetKasa.Application.Queries.CartWithItemsQuery;

public sealed record GetCartItemsByCartIdQuery(Guid Id) : IRequest<Result<List<CartItemDto>>>;

internal sealed class GetCartItemsByCartIdQueryHandler(ICartItemRepository cartItemRepository) : IRequestHandler<GetCartItemsByCartIdQuery, Result<List<CartItemDto>>>
{
    public async Task<Result<List<CartItemDto>>> Handle(GetCartItemsByCartIdQuery request, CancellationToken cancellationToken)
    {
        var items = await cartItemRepository
             .GetAll()
             .Where(c => c.CartId == request.Id)
             .Include(p => p.Product)
              .Select(ci => new CartItemDto
              {
                  Id = ci.Id,
                  ProductName = ci.Product.Name,
                  Price = ci.Price,
                  Quantity = ci.Quantity,
                  Discount = ci.Discount
              })
             .ToListAsync(cancellationToken);

        if (!items.Any())
        {
            return Result<List<CartItemDto>>.Failure("Sepette ürün bulunamadı!");
        }

        return Result<List<CartItemDto>>.Succeed(items);
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.CartItems;
using JetKasa.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace JetKasa.Application.Command.CartCommand;

public sealed record AddItemToCartCommand(Guid CartId, string Barcode, int Quantity) : IRequest<Result<string>>;

public sealed class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(x => x.CartId).NotEmpty().WithMessage("Sepet ID boş olamaz!");
        RuleFor(x => x.Barcode).NotEmpty().WithMessage("Barkod boş olamaz!");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır!");
    }
}

// Handler
internal sealed class AddItemToCartCommandHandler(
    ICartRepository cartRepository,
    IProductRepository productRepository,
    ICartItemRepository cartItemRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<AddItemToCartCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAllWithTracking()
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == request.CartId && c.Status == CartStatus.Active, cancellationToken);

        if (cart is null)
            return Result<string>.Failure("Aktif sepet bulunamadı!");

        var product = await productRepository.GetByExpressionWithTrackingAsync(
            p => p.Barcode == request.Barcode, cancellationToken);

        if (product is null)
            return Result<string>.Failure("Ürün bulunamadı.");

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;

            cartItemRepository.Update(existingItem);
        }
        else
        {
            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = request.Quantity,
                Price = product.Price,
                Discount = product.Discount ?? 0,
                Cart = cart,
                Product = product
            };

            await cartItemRepository.AddAsync(newCartItem);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Ürün sepete eklendi.");
    }
}

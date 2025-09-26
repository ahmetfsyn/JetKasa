using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Products;
using Mapster;
using MediatR;
using TS.Result;

namespace JetKasa.Application.Command.ProductCommand;

public sealed record CreateProductCommand(string Name, decimal Price, decimal Discount, string Barcode) : IRequest<Result<string>>;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Ürün adı boş olamaz.")
               .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter  olmalıdır.")
               .MaximumLength(50).WithMessage("Ürün adı en fazla 50 karakter olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürünün fiyatı 0'dan büyük olmalıdır.");

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100)
            .WithMessage("İndirim 0 ile 100 arasında olmalıdır.");

        RuleFor(x => x.Barcode)
            .NotEmpty().WithMessage("Barkod boş olamaz.")
            .Length(13).WithMessage("Barkod 13 haneli olmalıdır.");
    }
}

internal sealed class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var isBarcodeExists = await productRepository.AnyAsync(p => p.Barcode == request.Barcode, cancellationToken);

        if (isBarcodeExists)
        {
            return Result<string>.Failure("Bu barkod numarasına ait ürün mevcuttur!");
        }

        Product product = request.Adapt<Product>();

        await productRepository.AddAsync(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Ürün başarıyla eklendi.");
    }
}
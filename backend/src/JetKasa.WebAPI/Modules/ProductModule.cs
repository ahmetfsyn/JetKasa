using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Command.ProductCommand;
using JetKasa.Application.Queries.ProductQuery;
using JetKasa.Domain.Products;
using MediatR;
using TS.Result;

namespace JetKasa.WebAPI.Modules;

public static class ProductModule
{
    public static void RegisterProductRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/products").WithTags("Products");

        groupBuilder.MapPost("/create", async (ISender sender, CreateProductCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<Result<string>>();

        groupBuilder.MapPut("/update", async (ISender sender, UpdateProductCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<Result<string>>();

        groupBuilder.MapDelete("/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new DeleteProductCommand(id), cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<Result<string>>();

        groupBuilder.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetAllProductsQuery(), cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<Result<List<Product>>>();

        groupBuilder.MapGet("/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<Result<Product>>();
    }
}

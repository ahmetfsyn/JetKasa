using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Command.CartCommand;
using JetKasa.Application.Queries.CartWithItemsQuery;
using JetKasa.Domain.CartItems;
using JetKasa.Domain.Carts;
using MediatR;
using TS.Result;

namespace JetKasa.WebAPI.Modules
{
    public static class CartModule
    {
        public static void RegisterCartRoutes(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder groupBuilder = app.MapGroup("/cart").WithTags("Cart");

            groupBuilder.MapPost("/create", async (ISender sender, CreateCartCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);

                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);

            });

            groupBuilder.MapDelete("/cancel/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new CancelCartCommand(id), cancellationToken);

                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);

            }).Produces<Result<string>>();

            groupBuilder.MapPost("/complete/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new CompletedCartCommand(id), cancellationToken);

                return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);

            }).Produces<Result<string>>();

            groupBuilder.MapPost("/addItem", async (ISender sender, AddItemToCartCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
            }).Produces<Result<string>>();

            groupBuilder.MapGet("/{cartId:guid}/items", async (Guid cartId, ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetCartItemsByCartIdQuery(cartId), cancellationToken);

                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.NotFound(response);
            }).Produces<Result<CartItem>>();
        }

    }
}
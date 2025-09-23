using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Command.PaymentCommand;
using JetKasa.Application.Queries.PaymentQuery;
using MediatR;
using TS.Result;

namespace JetKasa.WebAPI.Modules
{
    public static class PaymentModule
    {
        public static void RegisterPaymentRoutes(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder groupBuilder = app.MapGroup("/payment").WithTags("Payment");

            groupBuilder.MapPost("/create/{CartId:guid}", async (ISender sender, Guid CartId, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new CreatePaymentByCartIdCommand(CartId), cancellationToken);

                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            }).Produces<Result<string>>();

            groupBuilder.MapGet("/{CartId:guid}", async (Guid CartId, ISender sender, CancellationToken cancellationToken) =>
           {
               var response = await sender.Send(new GetPaymentByCartIdQuery(CartId), cancellationToken);

               return response.IsSuccessful
                   ? Results.Ok(response)
                   : Results.NotFound(response);
           });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Dtos;
using JetKasa.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Novu;
using Novu.Models.Components;

namespace JetKasa.Infrastructure.Repositories;

public class NovuService : INovuService
{
    private readonly NovuSDK novu;

    public NovuService(IConfiguration configuration)
    {
        var secretKey = Environment.GetEnvironmentVariable("NOVU_SECRET_KEY") ?? configuration["Novu:SecretKey"];
        novu = new NovuSDK(secretKey: secretKey);
    }

    public async Task SendReceiptAsync(PaymentDto paymentDto)
    {
        string userName = paymentDto.UserName;
        string userEmail = paymentDto.UserEmail;

        string subscriberId = Guid.NewGuid().ToString();

        await novu.Subscribers.CreateAsync(new CreateSubscriberRequestDto
        {
            SubscriberId = subscriberId,
            FirstName = userName,
            Email = userEmail
        });

        var workflowId = "send-receipt";

        var itemsText = string.Join("<br>", paymentDto.CartDto.ItemDtos.Select(i =>
     {
         var unitPrice = i.Price;
         var quantity = i.Quantity;
         var discountPercent = i.Discount;
         var discountedUnitPrice = unitPrice * (1 - discountPercent);
         var totalPrice = discountedUnitPrice * quantity;
         var discountDisplay = discountPercent * 100;

         return $"{i.ProductName} {quantity} x {unitPrice:C2} İndirim: %{discountDisplay:0} Tutar: {totalPrice:C2}";
     }));

        string methodTr = paymentDto.Method switch
        {
            PaymentMethod.Card => "Kart",
            PaymentMethod.Cash => "Nakit",
            PaymentMethod.Mobile => "Mobil Ödeme",
            _ => paymentDto.Method.ToString()
        };


        var payload = new Dictionary<string, object>
    {
        { "paymentId", paymentDto.Id.ToString() },
        { "paidAt", paymentDto.PaidAt.ToString("dd.MM.yyyy HH:mm") },
        { "method", methodTr },
        { "total", paymentDto.Total.ToString("F2") },
        { "originalTotal", paymentDto.OriginalTotal.ToString("F2") },
        { "itemsText", itemsText }
    };
        var response = await novu.TriggerAsync(new TriggerEventRequestDto()
        {
            WorkflowId = workflowId,
            Payload = payload,
            To = To.CreateStr(subscriberId)
        });

        if (response is null)
        {
            throw new Exception("Novu bildirimi gönderilemedi");
        }
    }
}

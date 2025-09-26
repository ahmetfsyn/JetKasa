using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace JetKasa.WebAPI.Hubs
{
    public class PaymentHub : Hub
    {
        public async Task ConnectToPayment(Guid CartId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, CartId.ToString());
        }

        public async Task DisconnectFromPayment(Guid CartId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, CartId.ToString());
        }

    }
}
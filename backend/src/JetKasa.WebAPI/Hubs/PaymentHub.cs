
using Microsoft.AspNetCore.SignalR;

namespace JetKasa.WebAPI.Hubs
{

    public class PaymentHub : Hub
    {
        public async Task StartPayment(object data)
        {
            // POS cihazına mesaj gönderiyoruz.
            // Eğer POS tek device ise Clients.All yeterli
            await Clients.All.SendAsync("ReceivePaymentStart", data);
        }

        // Mobil cihazdan gelen veri
        public async Task PaymentProcessed(object resultData)
        {
            // Web uygulamasına gönder
            await Clients.All.SendAsync("ReceivePaymentResult", resultData);
        }
    }
}
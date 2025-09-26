using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetKasa.Domain.Dtos;

namespace JetKasa.Application.Interfaces
{
    public interface INovuService
    {
        Task SendReceiptAsync(PaymentDto paymentDto);
    }
}
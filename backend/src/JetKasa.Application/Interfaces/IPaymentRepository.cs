using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Domain.Payments;

namespace JetKasa.Application.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Payments;
using JetKasa.Infrastructure.Context;

namespace JetKasa.Infrastructure.Repositories
{
    internal sealed class PaymentRepository : Repository<Payment, AppDbContext>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
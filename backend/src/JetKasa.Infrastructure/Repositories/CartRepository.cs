using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Carts;
using JetKasa.Infrastructure.Context;

namespace JetKasa.Infrastructure.Repositories
{
    internal sealed class CartRepository : Repository<Cart, AppDbContext>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
    }
}
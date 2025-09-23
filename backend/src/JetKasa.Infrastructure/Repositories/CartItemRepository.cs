using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.CartItems;
using JetKasa.Infrastructure.Context;

namespace JetKasa.Infrastructure.Repositories
{
    internal sealed class CartItemREpository : Repository<CartItem, AppDbContext>, ICartItemRepository
    {
        public CartItemREpository(AppDbContext context) : base(context)
        {
        }
    }
}
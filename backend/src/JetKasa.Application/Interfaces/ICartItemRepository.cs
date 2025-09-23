using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Domain.CartItems;

namespace JetKasa.Application.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {

    }
}
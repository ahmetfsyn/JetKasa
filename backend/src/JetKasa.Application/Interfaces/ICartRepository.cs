using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Domain.Carts;

namespace JetKasa.Application.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {

    }
}
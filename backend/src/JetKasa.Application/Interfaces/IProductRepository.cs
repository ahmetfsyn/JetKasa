using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Domain.Products;

namespace JetKasa.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericRepository;
using JetKasa.Application.Interfaces;
using JetKasa.Domain.Products;
using JetKasa.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Query;

namespace JetKasa.Infrastructure.Repositories
{
    internal sealed class ProductRepository : Repository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

    }
}
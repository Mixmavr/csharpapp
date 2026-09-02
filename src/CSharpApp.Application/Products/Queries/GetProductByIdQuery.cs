using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Products.Queries
{
   public sealed record GetProductByIdQuery(int ProductId) : IRequest<Product?>;
}
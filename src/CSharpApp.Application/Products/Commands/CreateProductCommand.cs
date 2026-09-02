using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Products.Commands
{
    public sealed record CreateProductCommand(CreateProductDto Product) : IRequest<Product>;
    
}
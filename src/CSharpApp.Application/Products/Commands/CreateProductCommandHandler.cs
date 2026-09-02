using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Products.Commands
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductsApiClient _productsApiClient;

        public CreateProductCommandHandler(IProductsApiClient productsApiClient)
        {
            _productsApiClient = productsApiClient;
        }

        public Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return _productsApiClient.CreateProduct(request.Product, cancellationToken);
        }
    }
}
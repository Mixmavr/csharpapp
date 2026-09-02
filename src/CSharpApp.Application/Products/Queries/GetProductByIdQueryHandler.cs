using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Products.Queries
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductsApiClient _productsApiClient;

        public GetProductByIdQueryHandler(IProductsApiClient productsApiClient)
        {
            _productsApiClient = productsApiClient;
        }

        public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _productsApiClient.GetProductById(request.ProductId, cancellationToken);
        }
    }
}
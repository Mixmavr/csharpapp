using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using MediatR;

namespace CSharpApp.Application.Products.Queries;

public sealed class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, IReadOnlyCollection<Product>>
{
    private readonly IProductsApiClient _productsApiClient;

    public GetProductsQueryHandler(IProductsApiClient productsApiClient)
    {
        _productsApiClient = productsApiClient;
    }

    public Task<IReadOnlyCollection<Product>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        return _productsApiClient.GetProducts(cancellationToken);
    }
}
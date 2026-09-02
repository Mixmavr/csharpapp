using CSharpApp.Application.Products.Queries;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using Moq;

namespace CSharpApp.Application.Tests.Products.Queries;

public sealed class GetProductsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsProductsFromService()
    {
        IReadOnlyCollection<Product> expectedProducts =
        [
            new Product
            {
                Id = 1,
                Title = "First product"
            },
            new Product
            {
                Id = 2,
                Title = "Second product"
            }
        ];

        var productsApiClient = new Mock<IProductsApiClient>();

        productsApiClient
            .Setup(service => service.GetProducts(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);

        var handler = new GetProductsQueryHandler(productsApiClient.Object);

        var result = await handler.Handle(
            new GetProductsQuery(),
            CancellationToken.None);

        Assert.Same(expectedProducts, result);

        productsApiClient.Verify(
            service => service.GetProducts(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

using CSharpApp.Application.Products.Queries;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using Moq;

namespace CSharpApp.Application.Tests.Products.Queries;

public sealed class GetProductByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenProductExists_ReturnsProductFromApiClient()
    {
        var expectedProduct = new Product
        {
            Id = 40,
            Title = "Test product"
        };

        var productsApiClient = new Mock<IProductsApiClient>();

        productsApiClient
            .Setup(client => client.GetProductById(
                40,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProduct);

        var handler = new GetProductByIdQueryHandler(
            productsApiClient.Object);

        var result = await handler.Handle(
            new GetProductByIdQuery(40),
            CancellationToken.None);

        Assert.Same(expectedProduct, result);

        productsApiClient.Verify(
            client => client.GetProductById(
                40,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

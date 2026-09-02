using CSharpApp.Application.Products.Commands;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using Moq;

namespace CSharpApp.Application.Tests.Products.Commands;

public sealed class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenProductIsValid_ReturnsCreatedProductFromApiClient()
    {
        var createProductDto = new CreateProductDto
        {
            Title = "Desk Lamp",
            Price = 25,
            Description = "Warm white LED desk lamp",
            CategoryId = 1,
            Images = ["https://images.example.com/desk-lamp.jpg"]
        };

        var createdProduct = new Product
        {
            Id = 123,
            Title = createProductDto.Title,
            Price = createProductDto.Price
        };

        var productsApiClient = new Mock<IProductsApiClient>();

        productsApiClient
            .Setup(service => service.CreateProduct(
                createProductDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdProduct);

        var handler = new CreateProductCommandHandler(
            productsApiClient.Object);

        var result = await handler.Handle(
            new CreateProductCommand(createProductDto),
            CancellationToken.None);

        Assert.Same(createdProduct, result);

        productsApiClient.Verify(
            client => client.CreateProduct(
                createProductDto,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
using CSharpApp.Application.Categories.Queries;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using Moq;

namespace CSharpApp.Application.Tests.Categories.Queries;

public sealed class GetCategoryByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCategoryExists_ReturnsCategoryFromApiClient()
    {
        var expectedCategory = new Category
        {
            Id = 1,
            Name = "Clothes"
        };

        var categoriesApiClient = new Mock<ICategoriesApiClient>();

        categoriesApiClient
            .Setup(client => client.GetCategoryById(
                1,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategory);

        var handler = new GetCategoryByIdQueryHandler(
            categoriesApiClient.Object);

        var result = await handler.Handle(
            new GetCategoryByIdQuery(1),
            CancellationToken.None);

        Assert.Same(expectedCategory, result);

        categoriesApiClient.Verify(
            client => client.GetCategoryById(
                1,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
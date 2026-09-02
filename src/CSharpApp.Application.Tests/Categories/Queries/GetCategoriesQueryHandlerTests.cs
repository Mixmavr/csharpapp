using CSharpApp.Application.Categories.Queries;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using Moq;

namespace CSharpApp.Application.Tests.Categories.Queries;

public sealed class GetCategoriesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsCategoriesFromApiClient()
    {
        IReadOnlyCollection<Category> expectedCategories =
        [
            new Category
            {
                Id = 1,
                Name = "Clothes"
            },
            new Category
            {
                Id = 2,
                Name = "Electronics"
            }
        ];

        var categoriesApiClient = new Mock<ICategoriesApiClient>();

        categoriesApiClient
            .Setup(client => client.GetCategories(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategories);

        var handler = new GetCategoriesQueryHandler(
            categoriesApiClient.Object);

        var result = await handler.Handle(
            new GetCategoriesQuery(),
            CancellationToken.None);

        Assert.Same(expectedCategories, result);

        categoriesApiClient.Verify(
            client => client.GetCategories(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
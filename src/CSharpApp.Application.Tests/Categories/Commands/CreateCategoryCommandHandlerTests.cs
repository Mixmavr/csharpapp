using CSharpApp.Application.Categories.Commands;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Dtos.CategoriesDto;
using CSharpApp.Core.Interfaces;
using Moq;

namespace CSharpApp.Application.Tests.Categories.Commands;

public sealed class CreateCategoryCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenCategoryIsValid_ReturnsCreatedCategoryFromApiClient()
    {
        var createCategoryDto = new CreateCategoryDto
        {
            Name = "Books",
            Image = "https://images.example.com/books.jpg"
        };

        var createdCategory = new Category
        {
            Id = 10,
            Name = createCategoryDto.Name,
            Image = createCategoryDto.Image
        };

        var categoriesApiClient = new Mock<ICategoriesApiClient>();

        categoriesApiClient
            .Setup(client => client.CreateCategory(
                createCategoryDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCategory);

        var handler = new CreateCategoryCommandHandler(
            categoriesApiClient.Object);

        var result = await handler.Handle(
            new CreateCategoryCommand(createCategoryDto),
            CancellationToken.None);

        Assert.Same(createdCategory, result);

        categoriesApiClient.Verify(
            client => client.CreateCategory(
                createCategoryDto,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
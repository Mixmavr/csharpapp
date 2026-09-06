using CSharpApp.Application.Categories.Commands;
using CSharpApp.Application.Products.Commands;
using CSharpApp.Application.Validation;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Dtos.CategoriesDto;

namespace CSharpApp.Application.Tests.Validation;

public sealed class CommandValidatorTests
{
    [Fact]
    public void CreateProductCommand_WhenInputIsInvalid_HasValidationErrors()
    {
        var command = new CreateProductCommand(new CreateProductDto
        {
            Title = "",
            Price = 0,
            Description = "",
            CategoryId = 0,
            Images = ["not-a-url"]
        });

        var result = new CreateProductCommandValidator().Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "Product.Title");
        Assert.Contains(result.Errors, error => error.PropertyName == "Product.Price");
        Assert.Contains(result.Errors, error => error.PropertyName == "Product.Description");
        Assert.Contains(result.Errors, error => error.PropertyName == "Product.CategoryId");
        Assert.Contains(result.Errors, error => error.PropertyName.StartsWith("Product.Images"));
    }

    [Fact]
    public void CreateCategoryCommand_WhenInputIsValid_HasNoValidationErrors()
    {
        var command = new CreateCategoryCommand(new CreateCategoryDto
        {
            Name = "Books",
            Image = "https://example.com/books.jpg"
        });

        var result = new CreateCategoryCommandValidator().Validate(command);

        Assert.True(result.IsValid);
    }
}

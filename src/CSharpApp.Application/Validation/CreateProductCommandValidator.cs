using CSharpApp.Application.Products.Commands;
using FluentValidation;

namespace CSharpApp.Application.Validation;

public sealed class CreateProductCommandValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Product.Title)
            .NotEmpty();

        RuleFor(command => command.Product.Price)
            .GreaterThan(0);

        RuleFor(command => command.Product.Description)
            .NotEmpty();

        RuleFor(command => command.Product.CategoryId)
            .GreaterThan(0);

        RuleFor(command => command.Product.Images)
            .NotEmpty();

        RuleForEach(command => command.Product.Images)
            .Must(BeHttpUrl)
            .WithMessage("Each image must be a valid HTTP or HTTPS URL.");
    }

    private static bool BeHttpUrl(string image)
    {
        return Uri.TryCreate(image, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}
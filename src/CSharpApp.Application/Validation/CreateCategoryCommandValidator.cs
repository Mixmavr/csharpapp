using CSharpApp.Application.Categories.Commands;
using FluentValidation;

namespace CSharpApp.Application.Validation;

public sealed class CreateCategoryCommandValidator
    : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(command => command.Category.Name)
            .NotEmpty();

        RuleFor(command => command.Category.Image)
            .Must(BeHttpUrl)
            .WithMessage("Image must be a valid HTTP or HTTPS URL.");
    }

    private static bool BeHttpUrl(string image)
    {
        return Uri.TryCreate(image, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}
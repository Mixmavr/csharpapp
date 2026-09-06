using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Categories.Commands
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICategoriesApiClient _categoriesApiClient;

        public CreateCategoryCommandHandler(ICategoriesApiClient categoriesApiClient)
        {
            _categoriesApiClient = categoriesApiClient;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoriesApiClient.CreateCategory(request.Category, cancellationToken);
        }
    }
}
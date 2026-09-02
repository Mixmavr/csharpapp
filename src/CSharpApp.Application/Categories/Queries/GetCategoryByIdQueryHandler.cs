using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Categories.Queries
{
    public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        private readonly ICategoriesApiClient _categoriesApiClient;

        public GetCategoryByIdQueryHandler(ICategoriesApiClient categoriesApiClient)
        {
            _categoriesApiClient = categoriesApiClient;
        }

        public Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return _categoriesApiClient.GetCategoryById(request.CategoryId, cancellationToken);
        }
    }
}
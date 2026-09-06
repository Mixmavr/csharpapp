using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Application.Categories.Queries;
using MediatR;

namespace CSharpApp.Application.Categories.Queries
{
    public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyCollection<Category>>
    {
        private readonly ICategoriesApiClient _categoriesApiClient;

        public GetCategoriesQueryHandler(ICategoriesApiClient categoriesApiClient)
        {
            _categoriesApiClient = categoriesApiClient;
        }

        public async Task<IReadOnlyCollection<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesApiClient.GetCategories(cancellationToken);
        }
    }
}
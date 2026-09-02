using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Core.Dtos.CategoriesDto;

namespace CSharpApp.Core.Interfaces
{
    public interface ICategoriesApiClient
    {
        Task<IReadOnlyCollection<Category>> GetCategories(CancellationToken cancellationToken);

        Task<Category?> GetCategoryById(int categoryId, CancellationToken cancellationToken);

        Task<Category> CreateCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken);
    }
}
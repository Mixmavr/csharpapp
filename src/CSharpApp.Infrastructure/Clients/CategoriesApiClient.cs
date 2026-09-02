using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Dtos.CategoriesDto;

namespace CSharpApp.Infrastructure.Clients
{
    public sealed class CategoriesApiClient : ICategoriesApiClient
    {
        private readonly HttpClient _httpClient;

        public CategoriesApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Category>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>(
                "categories", cancellationToken)??[];

            return categories.AsReadOnly();            
        }

        public async Task<Category?> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"categories/{categoryId}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Category>(cancellationToken: cancellationToken);
        }

        public async Task<Category> CreateCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync("categories",categoryDto, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Category>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("The category API returned an empty response");
        }
    }
}
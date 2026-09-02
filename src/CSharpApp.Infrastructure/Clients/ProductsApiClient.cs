using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CSharpApp.Core.Dtos;

namespace CSharpApp.Infrastructure.Clients
{
    public class ProductsApiClient : IProductsApiClient
    {

        private readonly HttpClient _httpClient;

        public ProductsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("products", cancellationToken) ?? [];

            return products.AsReadOnly();
        }

        public async Task<Product?> GetProductById(int productId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"products/{productId}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Product>(cancellationToken: cancellationToken);
        
        
        }

        public async Task<Product> CreateProduct(CreateProductDto productDto, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync("products", productDto, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Product>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("The product API returned an empty response");
        }

        
       
    }
}
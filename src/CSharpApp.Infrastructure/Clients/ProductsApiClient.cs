using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IReadOnlyCollection<Product>> GetProducts()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("products") ?? [];

            return products.AsReadOnly();
        }
       
    }
}
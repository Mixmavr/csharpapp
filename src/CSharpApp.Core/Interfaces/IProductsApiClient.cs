using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpApp.Core.Interfaces
{
    public interface IProductsApiClient
    {
        Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken);

        Task<Product?> GetProductById(int productId, CancellationToken cancellationToken);

        Task<Product> CreateProduct(CreateProductDto productDto, CancellationToken cancellationToken);
    }
}
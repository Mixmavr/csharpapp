namespace CSharpApp.Application.Products;

public class ProductsService : IProductsService
{
    private readonly IProductsApiClient _productsApiClients;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(IProductsApiClient productsApiClient,
        ILogger<ProductsService> logger)
    {
        _productsApiClients = productsApiClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts()
    {
       return await _productsApiClients.GetProducts();
    }
}
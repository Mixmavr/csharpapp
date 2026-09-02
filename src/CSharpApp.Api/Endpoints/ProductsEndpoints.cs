using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Application.Products.Commands;
using CSharpApp.Application.Products.Queries;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using MediatR;

namespace CSharpApp.Api.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var versionedEndpoints = endpoints.NewVersionedApi();

            versionedEndpoints.MapGet("api/v{version:apiVersion}/getproducts", 
                async (ISender sender, CancellationToken cancellationToken) =>
                      
            {
                var products = await sender.Send(
                    new GetProductsQuery(),
                    cancellationToken);

                return Results.Ok(products);
            })
            .WithName("GetProducts")
            .HasApiVersion(1.0);

            versionedEndpoints.MapGet("api/v{version:apiVersion}/products/{productId:int}", 
                async (int productId, ISender sender, CancellationToken cancellationToken) =>                                                    
            {
                var product = await sender.Send(
                    new GetProductByIdQuery(productId),
                    cancellationToken);

                return product is null
                    ? Results.NotFound()
                    : Results.Ok(product);
            })
            .WithName("GetProductById")
            .HasApiVersion(1.0);

            versionedEndpoints.MapPost("api/v{version:apiVersion}/products",
            async (
                CreateProductDto createProductDto,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var product = await sender.Send(
                    new CreateProductCommand(createProductDto), cancellationToken);

                return Results.Created($"/api/v1/products/{product.Id}", product);
                    
                
            })
            .WithName("CreateProduct")
            .HasApiVersion(1.0);

            return endpoints;

            
            
        }
    }
}
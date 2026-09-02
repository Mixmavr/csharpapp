using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Application.Categories.Queries;
using MediatR;
using CSharpApp.Application.Categories.Commands;
using CSharpApp.Core.Dtos.CategoriesDto;

namespace CSharpApp.Api.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var versionedEndpoints = endpoints.NewVersionedApi();

            versionedEndpoints.MapGet("api/v{version:apiVersion}/categories",
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var categories = await sender.Send(new GetCategoriesQuery(), cancellationToken);

                return Results.Ok(categories);
            })
            .WithName("GetCategories")
            .HasApiVersion(1.0);

            versionedEndpoints.MapGet("api/v{version:apiVersion}/categories/{categoryId:int}",
            async (int categoryId, ISender sender, CancellationToken cancellationToken) =>
            {
                var category = await sender.Send(new GetCategoryByIdQuery(categoryId), cancellationToken);

                return category is null ? Results.NotFound() : Results.Ok(category);
            })
            .WithName("GetCategoryById")
            .HasApiVersion(1.0);

            versionedEndpoints.MapPost("api/v{version:apiVersion}/categories",
            async (CreateCategoryDto createCategoryDto, ISender sender, CancellationToken cancellationToken) =>
            {
                var category = await sender.Send(new CreateCategoryCommand(createCategoryDto), cancellationToken);

                return Results.Created($"/api/v1/categories/{category.Id}", category);
            })
            .WithName("CreateCategory")
            .HasApiVersion(1.0);

           return endpoints; 
        }
    }
}
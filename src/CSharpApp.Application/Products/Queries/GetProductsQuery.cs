using CSharpApp.Core.Dtos;
using MediatR;

namespace CSharpApp.Application.Products.Queries;

public sealed record GetProductsQuery : IRequest<IReadOnlyCollection<Product>>;
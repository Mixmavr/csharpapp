using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos
{
    public sealed class CreateProductDto
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("price")]
    public required int Price { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("categoryId")]
    public required int CategoryId { get; init; }

    [JsonPropertyName("images")]
    public required List<string> Images { get; init; }
}
}
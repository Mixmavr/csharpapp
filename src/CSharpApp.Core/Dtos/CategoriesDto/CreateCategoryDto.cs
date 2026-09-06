using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.CategoriesDto
{
   public sealed class CreateCategoryDto
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("image")]
    public required string Image { get; init; }
}
}
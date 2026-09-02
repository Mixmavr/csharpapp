using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.CategoriesDto
{
    public sealed class CreateCategoryDto
    {
        public required string Name { get; init; }

        public required string Image { get; init; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos
{
    public sealed class CreateProductDto
    {
        public required string Title { get; init; }

        public required int Price { get; init; }

        public required string Description { get; init; }

        public required int CategoryId { get; init; }

        public required List<string> Images { get; init; }
    }
}
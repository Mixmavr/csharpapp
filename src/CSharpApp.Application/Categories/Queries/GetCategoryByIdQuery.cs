using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CSharpApp.Application.Categories.Queries
{
    public sealed record GetCategoryByIdQuery(int CategoryId) : IRequest<Category?>;
}
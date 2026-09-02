using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Core.Dtos.CategoriesDto;
using MediatR;

namespace CSharpApp.Application.Categories.Commands
{
    public sealed record CreateCategoryCommand(CreateCategoryDto Category) : IRequest<Category>;
}
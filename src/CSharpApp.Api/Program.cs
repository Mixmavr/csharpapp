using CSharpApp.Application.Products.Queries;
using MediatR;
using CSharpApp.Api.Endpoints;
using CSharpApp.Api.Middleware;
using CSharpApp.Application.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.ClearProviders().AddSerilog(logger);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<GetProductByIdQueryHandler>();
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();

builder.Services.AddDefaultConfiguration();
builder.Services.AddHttpConfiguration();
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

//Performance Middleware
app.UseMiddleware<RequestPerformanceMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.MapProductsEndpoints();
app.MapCategoriesEndpoints();

app.Run();
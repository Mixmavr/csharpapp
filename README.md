# C# Accepted Assessment app

An application for C# (.net) knowledge assessment

## Description

This is a web application that interacts with a 3nd party service (<https://fakeapi.platzi.com>/<https://api.escuelajs.co>) and serves data

We have to do some code refactoring and implement some new features

## Code refactoring

Seems that the use of http client is not so much efficient

Let's make a different, more solid, approach/implementation

## New features

**#1**

Right now only the **getAll** method supported for **products**

We have to implement **getOne** and **create** methods also

**#2**

Add implementation for **categories**

**#3**

3nd party service supports JWT auth. We have to implement and support it. Use the credentials provided to appsettings.json file.

**#4**

We must measure and log the performance of the requests. Create a middleware to achieve this.

## Implementation

* Try to understand and keep the architectural approach.
* Add unit testing.
* Add docker support.
* Using CQRS pattern will be considered as a strong plus.
* The attached collections (postman/insomnia) will help you with the requests.

## Current implementation

The application uses a layered architecture with the following responsibilities:

* `CSharpApp.Api`: Minimal API endpoints, middleware and request pipeline.
* `CSharpApp.Application`: CQRS queries, commands, handlers and validation.
* `CSharpApp.Core`: DTOs, settings and external API contracts.
* `CSharpApp.Infrastructure`: typed HTTP clients, JWT authentication and HTTP configuration.

External API calls use typed `HttpClient` instances registered through `IHttpClientFactory`. Products and categories use MediatR handlers and the `IProductsApiClient`/`ICategoriesApiClient` abstractions for outbound communication.

JWT authentication is handled internally when calling the third-party API. The application uses the configured credentials to obtain an access token, caches it in memory, and adds it as a Bearer token to product and category requests. The token cache is thread-safe, preventing duplicate login requests when multiple requests arrive at the same time. The token is not exposed through a local login endpoint.

Create commands are validated before reaching the external API. Product validation checks required values, positive price/category ID, images and HTTP/HTTPS image URLs. Category validation checks the name and image URL. Invalid commands return HTTP 400 validation responses.

Request performance is measured by `RequestPerformanceMiddleware`. Each request logs its method, path, status code and elapsed milliseconds. Requests exceeding `PerformanceSettings:SlowRequestThresholdMilliseconds` are logged as warnings. The default threshold is 500 ms.

Unit tests cover product and category handlers, command validation and access-token caching.

## How to run

### Local development

Prerequisites: .NET 9 SDK

```bash
cd C:\Projects\csharpapp
dotnet restore src/CSharpApp.sln
dotnet build src/CSharpApp.sln
dotnet test src/CSharpApp.sln
dotnet run --project src/CSharpApp.Api/CSharpApp.Api.csproj
```

The API runs on `http://localhost:5225`.

The external API base URL and authentication credentials are configured under `RestApiSettings` in `src/CSharpApp.Api/appsettings.json`. For a public repository, use environment variables or user secrets for credentials instead of committing real secrets.

### Docker

Prerequisites: Docker Desktop

Build:
```bash
docker build -t csharpapp-api .
```

Run:
```bash
docker run -d -p 8080:8080 csharpapp-api
```

The API runs on `http://localhost:8080`.

To view container logs:

```bash
docker logs -f <container_id>
```

To stop and remove the container:

```bash
docker rm -f <container_id>
```

### Endpoints

Products:
- `GET /api/v1/getproducts` — get all products
- `GET /api/v1/products/{id}` — get product by ID
- `POST /api/v1/products` — create product

Categories:
- `GET /api/v1/categories` — get all categories
- `GET /api/v1/categories/{id}` — get category by ID
- `POST /api/v1/categories` — create category

Performance:
- Request duration and status code are logged to console for each request.
- Requests slower than the configured threshold are logged as warnings.

### Validation example

Invalid create requests are rejected locally before an external request is sent. For example, a product with an empty title, a non-positive price or an invalid image URL returns HTTP 400.

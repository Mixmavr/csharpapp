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

## How to run

### Local development

Prerequisites: .NET 9.0 SDK

```bash
cd C:\Projects\csharpapp
dotnet restore src/CSharpApp.sln
dotnet build src/CSharpApp.sln
dotnet test src/CSharpApp.sln
dotnet run --project src/CSharpApp.Api/CSharpApp.Api.csproj
```

The API runs on `http://localhost:5225`.

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

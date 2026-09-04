FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/CSharpApp.sln ./src/
COPY src/CSharpApp.Api/CSharpApp.Api.csproj ./src/CSharpApp.Api/
COPY src/CSharpApp.Application/CSharpApp.Application.csproj ./src/CSharpApp.Application/
COPY src/CSharpApp.Application.Tests/CSharpApp.Application.Tests.csproj ./src/CSharpApp.Application.Tests/
COPY src/CSharpApp.Core/CSharpApp.Core.csproj ./src/CSharpApp.Core/
COPY src/CSharpApp.Infrastructure/CSharpApp.Infrastructure.csproj ./src/CSharpApp.Infrastructure/

RUN dotnet restore ./src/CSharpApp.sln

COPY src/. ./src/

RUN dotnet publish ./src/CSharpApp.Api/CSharpApp.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CSharpApp.Api.dll"]

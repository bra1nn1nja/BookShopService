#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BookShopService.API/BookShopService.API.csproj", "src/BookShopService.API/"]
COPY ["src/BookShopService.Domain/BookShopService.Domain.csproj", "src/BookShopService.Domain/"]
COPY ["src/BookShopService.Infrastructure/BookShopService.Infrastructure.csproj", "src/BookShopService.Infrastructure/"]
RUN dotnet restore "src/BookShopService.API/BookShopService.API.csproj"
COPY . .
WORKDIR "/src/src/BookShopService.API"
RUN dotnet build "BookShopService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShopService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShopService.API.dll", "--environment=staging"]
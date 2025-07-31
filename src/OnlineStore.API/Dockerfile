# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnlineStore.API/OnlineStore.API.csproj", "OnlineStore.API/"]
COPY ["OnlineStore.Application/OnlineStore.Application.csproj", "OnlineStore.Application/"]
COPY ["OnlineStoreAPI.Domain/OnlineStoreAPI.Domain.csproj", "OnlineStoreAPI.Domain/"]
COPY ["Shared.Kernel/OnlineStoreAPI.Shared.Kernel/OnlineStoreAPI.Shared.Kernel.csproj", "Shared.Kernel/OnlineStoreAPI.Shared.Kernel/"]
COPY ["OnlineStore.Infrastructure/OnlineStore.Infrastructure.csproj", "OnlineStore.Infrastructure/"]
RUN dotnet restore "./OnlineStore.API/OnlineStore.API.csproj"
COPY . .
WORKDIR "/src/OnlineStore.API"
RUN dotnet build "./OnlineStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.API.dll"]
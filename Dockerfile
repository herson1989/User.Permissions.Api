FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User.Permissions.Api/User.Permissions.Api.csproj", "User.Permissions.Api/"]
COPY ["Application/User.Permissions.Application.csproj", "Application/"]
COPY ["User.Permissions.Domain/User.Permissions.Domain.csproj", "User.Permissions.Domain/"]
COPY ["User.Permissions.Infrastructure/User.Permissions.Infrastructure.csproj", "User.Permissions.Infrastructure/"]
RUN dotnet restore "./User.Permissions.Api/User.Permissions.Api.csproj"
COPY . .
WORKDIR "/src/User.Permissions.Api"
RUN dotnet build "./User.Permissions.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./User.Permissions.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Permissions.Api.dll"]
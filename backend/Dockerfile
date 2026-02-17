# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["ProductManagementApi/ProductManagementApi.csproj", "ProductManagementApi/"]
RUN dotnet restore "ProductManagementApi/ProductManagementApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/ProductManagementApi"
RUN dotnet build "ProductManagementApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ProductManagementApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 80

ENTRYPOINT ["dotnet", "ProductManagementApi.dll"]

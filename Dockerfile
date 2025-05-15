# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyBackendApi.csproj", "./"]
RUN dotnet restore "./MyBackendApi.csproj"
COPY . .
RUN dotnet build "MyBackendApi.csproj" -c Release -o /app/build
RUN dotnet publish "MyBackendApi.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyBackendApi.dll"]

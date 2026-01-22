# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["Sostav/Sostav.csproj", "Sostav/"]
COPY ["Sostav.Application/Sostav.Application.csproj", "Sostav.Application/"]
COPY ["Sostav.Domain/Sostav.Domain.csproj", "Sostav.Domain/"]
COPY ["Sostav.Infrastructure/Sostav.Infrastructure.csproj", "Sostav.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Sostav/Sostav.csproj"

# Copy everything else
COPY . .

# Build
WORKDIR "/src/Sostav"
RUN dotnet build "./Sostav.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sostav.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy published files
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Sostav.dll"]
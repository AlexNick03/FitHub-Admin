# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- CALEA NOUA (Cu folderul FitHubAdmin.API) ---
COPY ["FitHubAdmin/FitHubAdmin.API/FitHubAdmin.API.csproj", "FitHubAdmin/FitHubAdmin.API/"]
RUN dotnet restore "FitHubAdmin/FitHubAdmin.API/FitHubAdmin.API.csproj"

# Copiem tot
COPY . .

# --- Ne mutam in folderul corect pentru a da Build ---
WORKDIR "/src/FitHubAdmin/FitHubAdmin.API"
RUN dotnet build "FitHubAdmin.API.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "FitHubAdmin.API.csproj" -c Release -o /app/publish

# Etapa 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitHubAdmin.API.dll"]
# Imagen base con runtime para ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen con SDK para construir la app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["NewsPortal.csproj", "./"]
RUN dotnet restore "NewsPortal.csproj"

# Copiar el resto de archivos y compilar en modo Release
COPY . .
RUN dotnet publish "NewsPortal.csproj" -c Release -o /app/publish

# Imagen final con la app publicada lista para correr
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NewsPortal.dll"]

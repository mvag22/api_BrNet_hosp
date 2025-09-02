# Etapa 1 - Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos do projeto
COPY *.sln .
COPY ApiBrnetEstoque/*.csproj ./ApiBrnetEstoque/
RUN dotnet restore

COPY ApiBrnetEstoque/. ./ApiBrnetEstoque/
WORKDIR /src/ApiBrnetEstoque
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 - Runtime (imagem menor)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiBrnetEstoque.dll"]

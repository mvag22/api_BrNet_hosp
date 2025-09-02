# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos da solu��o e csproj
COPY *.sln ./
COPY *.csproj ./

# Restaura depend�ncias
RUN dotnet restore

# Copia todo o restante do projeto
COPY . ./

# Publica a aplica��o em modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiBrnetEstoque.dll"]

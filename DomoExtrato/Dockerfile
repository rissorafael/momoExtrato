# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["DomoExtrato/DomoExtrato.csproj", "DomoExtrato/"]

# Restaurar dependências
RUN dotnet restore "./DomoExtrato/DomoExtrato.csproj"
COPY . .

# Compilar a aplicação
WORKDIR "/src/DomoExtrato"
RUN dotnet build "./DomoExtrato.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "./DomoExtrato.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN apt-get update && apt-get install -y \
    ttf-mscorefonts-installer \
    && rm -rf /var/lib/apt/lists/*

# Instalar o wkhtmltopdf e suas dependências
RUN apt-get update && apt-get install -y \
    wkhtmltopdf \
    libx11-6 \
    libxrender1 \
    libfontconfig1 \
    libxext6 \
    libfreetype6 \
    libssl1.1 \
    libjpeg62-turbo \
    libpng16-16 \
    libgcc1 \
    libatk1.0-0 \
    libcups2 \
    libwkhtmltox0 \
    && rm -rf /var/lib/apt/lists/*

# Definir diretório de trabalho e copiar os arquivos publicados
WORKDIR /app
COPY --from=publish /app/publish .

# Definir o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "DomoExtrato.dll"]
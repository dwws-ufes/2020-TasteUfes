FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

# Exibir a versão do .NET
RUN dotnet --version

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o out

# EF Core
RUN dotnet tool install --global dotnet-ef --version 5.0.4
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef database update

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/*.db .

## Exposição das portas 80 e 443
EXPOSE 80
EXPOSE 443

## Dependências
RUN apt update
RUN apt install sqlite3 libsqlite3-dev -y
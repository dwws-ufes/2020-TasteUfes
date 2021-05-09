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
# RUN dotnet tool install --global dotnet-ef --version 5.0.5
# ENV PATH="${PATH}:/root/.dotnet/tools"
# RUN dotnet ef database update

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

## Exposição de portas
EXPOSE 80
EXPOSE 443

## Dependências
RUN apt update

# Wait for it
COPY ./wait-for-it.sh /wait-for-it.sh
RUN apt install -y dos2unix
RUN dos2unix /wait-for-it.sh
RUN chmod +x /wait-for-it.sh
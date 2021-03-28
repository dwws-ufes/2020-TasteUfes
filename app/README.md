## Preparação do Ambiente

### Requisitos:
- [.NET Core 5.0 SDK (ou 3.1)](https://dotnet.microsoft.com/download/dotnet/5.0)
- dotnet-ef (EF Core Tools)

Para instalar o EF Core Tools, execute o seguinte comando:
```bash
dotnet tool install --global dotnet-ef
```

### Banco de dados:
Por fins de praticidade, foi escolhido o banco de dados SQLite, que se resume a um único arquivo, não necessitando de nenhuma engine instalada. SQLite possui algumas limitações de funcionalidades e performance se comparado a outros bancos (MySQL, PostgreSQL, OracleDB, etc.), mas atende perfeitamente às necessidades do trabalho.

Para executar as migrations e criar/atualizar o banco de dados, é necessário executar o seguinte comando:
```bash
dotnet ef database update
```

## Execução da API
Para executar a API, utilize o comando:
```bash
dotnet run
```
A API ficará disponível nas rotas
`http://localhost:5000/api/v1` e `https://localhost:5001/api/v1`

## Documentação da API
A documentação é feita em Swagger e está disponível em: `localhost:<PORT>/swagger/index.html`.
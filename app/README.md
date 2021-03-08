## Preparação do Ambiente

### Requisitos:
- .NET Core 3.1 SDK (ou 5.0)
- dotnet-ef (EF Core Tools)

### Banco de dados:
Por fins de praticidade, foi escolhido o banco de dados SQLite, que se resume a um único arquivo, não necessitando de nenhuma engine instalada. SQLite possui algumas limitações de funcionalidades e performance se comparado a outros bancos como MySQL, PostgreSQL, OracleDB e outros.

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
A documentação da API será feita com o Swagger, e em breve estará disponível em `localhost:<PORT>/doc`.
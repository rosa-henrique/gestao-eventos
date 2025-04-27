<div align="center">

[![Build](https://github.com/rosa-henrique/gestao-eventos/actions/workflows/build.yml/badge.svg)](https://github.com/rosa-henrique/gestao-eventos/actions/workflows/build.yml)
[![GitHub license](./assets/license.svg)](https://github.com/seu-usuario/seu-projeto/blob/main/LICENSE)  
[![codecov](https://codecov.io/gh/rosa-henrique/gestao-eventos/graph/badge.svg?token=GYB8E8MWV3)](https://codecov.io/gh/rosa-henrique/gestao-eventos)

---  

Gestão eventos

---  

</div>

# Visão Geral

O **EventFlow** é um sistema de gestão de eventos onde os usuários podem:
- **Organizar eventos** (criar, editar, cancelar).
- **Comprar ingressos** (com validação de disponibilidade e pagamentos integrados).

## Arquitetura e Padrões
- **Domain-Driven Design (DDD)**: Domínio rico com entidades, aggregates e domain events para garantir consistência eventual.
- **Clean Architecture**: Separação clara em camadas (Domain, Application, Infrastructure, Presentation).
- **CQRS**: Segregação entre comandos (escrita) e consultas (leitura) usando MediatR.
- **Domain Events**: Para ações assíncronas (ex: enviar e-mail após compra de ingresso).

## Tecnologias Principais
- **Backend**: .NET 8, Entity Framework Core (PostgreSQL), MediatR, ErrorOr (para tratamento de erros).
- **Monitoramento**: Serilog (logs estruturados) + Seq (visualização).
- **Testes**: XUnit (unit/integration) + NSubstitute (mocks).

```shell
# Exemplo de instalação (ajuste conforme seu projeto):
dotnet run --project src/GestaoEventos.Api
```

# Funcionalidades 🚀

## Autenticação 🔐
- **Criar usuário**: Registro inicial no sistema.
- **Gerar token JWT**: Autenticação segura para acesso às funcionalidades.

```shell
# Exemplo: Gerar token
POST /auth/login
{
    "email": "usuario@exemplo.com",
    "senha": ""
}
```

## Eventos 🎪
### Escrita (Comandos)
- **Adicionar evento**: Criar novos eventos.
- **Adicionar sessão**: Vincular horários a eventos.
- **Adicionar ingresso**: Definir tipos de ingressos.
- **Alterar/Cancelar evento**: Atualizar detalhes ou status.
- **Remover sessão/ingresso**: Exclusão de registros.

```shell
# Exemplo: Criar evento
POST /eventos
{
  "nome": "string",
  "dataHoraInicio": "2025-04-25T12:00:00.000Z",
  "dataHoraFim": "2025-04-29T20:00:00.000Z",
  "localizacao": "string",
  "capacidadeMaxima": 100
}
```
### Leitura (Consultas)
- **Buscar evento por ID**: Detalhes específicos.
- **Listar eventos**: Listar todos eventos disponiveis.
- **Buscar sessões/ingressos**: Relacionados a um evento.
```shell
# Exemplo: Listar eventos
GET /eventos
```

## Compras 🎟️
- **Comprar ingresso**: Vinculado a uma sessão específica.
```shell
# Exemplo: Comprar ingresso
POST /compras/ingressos/:sessaoId/
[
	{
		"ingressoId": "59c907d2-d47b-4338-a89d-6ba9ac1a1940",
		"quantidade": 3
	},
	{
		"ingressoId": "ea58d33e-682e-4411-94ba-9d09f664611c",
		"quantidade": 3
	}
]
```

# Como executar:
#### 1. Iniciar os serviços
```shell
docker-compose up -d --build
```

#### 2. Executar Migrations
```shell
# Dentro do container (recomendado para produção)
docker exec -it gestao_eventos_api bash -c "cd /app/src/GestaoEventos.Infrastructure && dotnet ef database update --startup-project ../GestaoEventos.Api"

# Localmente (desenvolvimento)
cd src/GestaoEventos.Infrastructure
dotnet ef database update --startup-project ../GestaoEventos.Api
```

### Serviços disponíveis:
| Serviço  | URL                          | Portas      |
|----------|------------------------------|-------------|
| PostgreSQL | `host=localhost;port=5432` | 5432        |
| Seq (Logs) | http://localhost:5341      | 5341, 8081  |
| API       | http://localhost:5271      | 5271, 7211  |

# Demais informações
- [Instruções de como rodar local e preparar ambiente para novas feature](./run.MD)

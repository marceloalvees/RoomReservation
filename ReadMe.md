# Documentação do Sistema de Controle de Reservas

## Visão Geral
O sistema de controle de reservas é uma aplicação desenvolvida para uma empresa de coworking, permitindo a gestão de reservas de salas de reunião. A solução é composta por uma aplicação **ASP.NET Core MVC** que consome uma **Web API** para realizar operações CRUD sobre as reservas.

## Arquitetura do Sistema
O projeto segue os princípios da **Arquitetura Limpa (Clean Architecture)** e do **Domain-Driven Design (DDD)**, sendo dividido nos seguintes componentes:

1. **Domínio**: Contém as entidades e regras de negócio.
2. **Aplicação**: Inclui os casos de uso (**Use Cases**), estruturados em **Commands e Queries**, utilizando o padrão **Mediator** para orquestração.
3. **Infraestrutura**: Responsável pela persistência de dados e comunicação com o banco de dados. Também contém um **serviço dedicado para envio de e-mails de confirmação**.
4. **Apresentação**:
   - **MVC**: Interface web para os usuários gerenciarem as reservas.
   - **Web API**: Interface para operações do sistema.

### Referências entre Camadas
- **Application** referencia **Domain**.
- **Infrastructure** referencia **Application** e **Domain**.
- **API** referencia **Application**, **Infrastructure**, e **Domain**.
- **MVC** consome a **API** (via `HttpClient`).

## Tecnologias Utilizadas
- **ASP.NET Core 6+** (MVC e Web API)
- **Entity Framework Core 6+**
- **SQL Server ou PostgreSQL**
- **Injeção de Dependência**
- **Padrão Repository e Unit of Work**
- **Testes Unitários (xUnit/NUnit)**

## Regras de Negócio
1. **Uma sala só pode ser reservada se não houver conflitos de horário** com outras reservas.
2. **Uma reserva só pode ser cancelada com no mínimo 24 horas de antecedência**.

## Estrutura do Banco de Dados
### Tabela `Salas`
| ID  | Nome     | Capacidade | Localização |
|-----|---------|------------|------------|
| 1   | Sala A  | 10         |Sede	   |
| 2   | Sala B  | 8          |Filial 1   |

### Tabela `Usuarios`
| ID  | Nome     | Email     | PhoneNumber|
|-----|---------|------------|------------|
| 1   | user1  | email      |8198454503  |
| 2   | user2  | email      |8154664003  |

### Tabela `Reservas`
| ID  | SalaID | UsuarioID | DataHora           | Status      | DataCancelamento |
|-----|--------|----------|--------------------|------------|------------------|
| 1   | 1      | 101      | 2024-02-15 10:00   | Confirmada |                  | 
| 2   | 2      | 102      | 2024-02-15 11:00   | Cancelada  | 15/02/2024       |

## Fluxo das Reservas
1. O usuário acessa a interface **MVC**.
2. Realiza uma nova reserva escolhendo a **sala e horário**.
3. O sistema verifica conflitos e **salva a reserva**.
4. O usuário recebe um **e-mail de confirmação**.
5. Se necessário, o usuário pode **editar ou cancelar** a reserva.

## Decisões de Design
- **Uso do Padrão Repository e Unit of Work**: Para melhor gestão da persistência de dados e separação entre camadas.
- **Injeção de Dependência**: Facilitando a manutenção e os testes.
- **Factory Pattern**: Para criação padronizada de objetos.
- **Mediator Pattern**: Para comunicação desacoplada entre componentes, utilizando **Commands e Queries nos casos de uso (Use Cases)**.
- **Service Layer na Infraestrutura**: Implementado um **serviço de envio de e-mails** para confirmação de reservas.

## Testes
- **Testes Unitários** para regras de negócio.
- **Testes de Integração** para verificação da comunicação entre camadas.
- **Cobertura de Testes**: Objetivo de pelo menos **80% das funcionalidades cobertas**.

## Conclusão
Este projeto implementa um sistema de reservas seguindo boas práticas de desenvolvimento e arquitetura. A utilização de padrões como **Repository, Unit of Work e Mediator** proporciona um código modular, testável e de fácil manutenção. Além disso, a infraestrutura conta com um **serviço de envio de e-mails** para garantir a comunicação eficaz com os usuários.

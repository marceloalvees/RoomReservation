# Documenta��o do Sistema de Controle de Reservas

## Vis�o Geral
O sistema de controle de reservas � uma aplica��o desenvolvida para uma empresa de coworking, permitindo a gest�o de reservas de salas de reuni�o. A solu��o � composta por uma aplica��o **ASP.NET Core MVC** que consome uma **Web API** para realizar opera��es CRUD sobre as reservas.

## Arquitetura do Sistema
O projeto segue os princ�pios da **Arquitetura Limpa (Clean Architecture)** e do **Domain-Driven Design (DDD)**, sendo dividido nos seguintes componentes:

1. **Dom�nio**: Cont�m as entidades e regras de neg�cio.
2. **Aplica��o**: Inclui os casos de uso (**Use Cases**), estruturados em **Commands e Queries**, utilizando o padr�o **Mediator** para orquestra��o.
3. **Infraestrutura**: Respons�vel pela persist�ncia de dados e comunica��o com o banco de dados. Tamb�m cont�m um **servi�o dedicado para envio de e-mails de confirma��o**.
4. **Apresenta��o**:
   - **MVC**: Interface web para os usu�rios gerenciarem as reservas.
   - **Web API**: Interface para opera��es do sistema.

### Refer�ncias entre Camadas
- **Application** referencia **Domain**.
- **Infrastructure** referencia **Application** e **Domain**.
- **API** referencia **Application**, **Infrastructure**, e **Domain**.
- **MVC** consome a **API** (via `HttpClient`).

## Tecnologias Utilizadas
- **ASP.NET Core 6+** (MVC e Web API)
- **Entity Framework Core 6+**
- **SQL Server ou PostgreSQL**
- **Inje��o de Depend�ncia**
- **Padr�o Repository e Unit of Work**
- **Testes Unit�rios (xUnit/NUnit)**

## Regras de Neg�cio
1. **Uma sala s� pode ser reservada se n�o houver conflitos de hor�rio** com outras reservas.
2. **Uma reserva s� pode ser cancelada com no m�nimo 24 horas de anteced�ncia**.

## Estrutura do Banco de Dados
### Tabela `Salas`
| ID  | Nome     | Capacidade | Localiza��o |
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
1. O usu�rio acessa a interface **MVC**.
2. Realiza uma nova reserva escolhendo a **sala e hor�rio**.
3. O sistema verifica conflitos e **salva a reserva**.
4. O usu�rio recebe um **e-mail de confirma��o**.
5. Se necess�rio, o usu�rio pode **editar ou cancelar** a reserva.

## Decis�es de Design
- **Uso do Padr�o Repository e Unit of Work**: Para melhor gest�o da persist�ncia de dados e separa��o entre camadas.
- **Inje��o de Depend�ncia**: Facilitando a manuten��o e os testes.
- **Factory Pattern**: Para cria��o padronizada de objetos.
- **Mediator Pattern**: Para comunica��o desacoplada entre componentes, utilizando **Commands e Queries nos casos de uso (Use Cases)**.
- **Service Layer na Infraestrutura**: Implementado um **servi�o de envio de e-mails** para confirma��o de reservas.

## Testes
- **Testes Unit�rios** para regras de neg�cio.
- **Testes de Integra��o** para verifica��o da comunica��o entre camadas.
- **Cobertura de Testes**: Objetivo de pelo menos **80% das funcionalidades cobertas**.

## Conclus�o
Este projeto implementa um sistema de reservas seguindo boas pr�ticas de desenvolvimento e arquitetura. A utiliza��o de padr�es como **Repository, Unit of Work e Mediator** proporciona um c�digo modular, test�vel e de f�cil manuten��o. Al�m disso, a infraestrutura conta com um **servi�o de envio de e-mails** para garantir a comunica��o eficaz com os usu�rios.

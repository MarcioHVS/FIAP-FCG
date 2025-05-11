# FIAP Cloud Games (FCG)
## _Plataforma de jogos digitais_

O FIAP Cloud Games (FCG) é um projeto acadêmico que reúne conhecimentos adquiridos nas disciplinas onde o desafio envolve o desenvolvimento da plataforma FIAP Cloud Games (FCG), que permitirá a venda de jogos digitais e a gestão de servidores para partidas online.
O projeto tem como foco a criação de uma API REST em .NET 8 para gerenciar usuários e suas bibliotecas de jogos adquiridos, garantindo persistência de dados, qualidade do software e boas práticas de desenvolvimento.

## 📋 Pré-requisitos

Antes de iniciar o projeto, é necessário atender aos seguintes pré-requisitos para garantir um ambiente de desenvolvimento adequado:

### 🛠 Tecnologias Necessárias
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) – Plataforma de desenvolvimento para criar a API REST
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) – Banco de dados para persistência dos dados
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/) ou [VS Code](https://code.visualstudio.com/) – IDE recomendada para desenvolvimento

### 📦 Pacotes e Dependências

O projeto está organizado em camadas e depende dos seguintes pacotes:

#### Camada Api
- Autenticação via JWT: Microsoft.AspNetCore.Authentication.JwtBearer, Microsoft.IdentityModel.Tokens
- ORM para banco de dados: Microsoft.EntityFrameworkCore.Design
- Documentação da API: Swashbuckle.AspNetCore

```
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.15
Install-Package Microsoft.IdentityModel.Tokens -Version 8.9.0
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.15
Install-Package Swashbuckle.AspNetCore -Version 7.3.2
```

#### Camada Application
- Segurança e geração de tokens JWT: Microsoft.IdentityModel.Tokens, System.IdentityModel.Tokens.Jwt
```
Install-Package Microsoft.IdentityModel.Tokens -Version 8.9.0
Install-Package System.IdentityModel.Tokens.Jwt -Version 8.9.0
```

#### Camada Domain
- Criptografia de senhas com Argon2: Isopoh.Cryptography.Argon2
```
Install-Package Isopoh.Cryptography.Argon2 -Version 2.0.0
```

#### Camada Infrastructure
- ORM e consultas de alta performance: Microsoft.EntityFrameworkCore, Dapper
- Banco de dados SQL Server: Microsoft.EntityFrameworkCore.SqlServer
- Ferramentas de migração: Microsoft.EntityFrameworkCore.Tools
```
Install-Package Dapper -Version 2.1.66
Install-Package Microsoft.EntityFrameworkCore -Version 8.0.15
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.15
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.15
```

#### Camada Tests
- Framework de testes unitários: xunit, Moq, Microsoft.NET.Test.Sdk
- Cobertura de código: coverlet.collector
- Banco de testes em memória: Microsoft.EntityFrameworkCore.Sqlite
```
Install-Package coverlet.collector -Version 6.0.4
Install-Package Microsoft.EntityFrameworkCore.Sqlite -Version 8.0.15
Install-Package Microsoft.NET.Test.Sdk -Version 17.13.0
Install-Package Moq -Version 4.20.72
Install-Package xunit -Version 2.9.3
Install-Package xunit.runner.visualstudio -Version 3.1.0
```

## 🗂️ Estrutura da Api
```
FCG/
│──📂 FCG.Api/
│   ├──📂 Configurations/
│   ├──📂 Controllers/
│   ├──📂 Middleware/
│──📂 FCG.Application/
│   ├──📂 DTOs/
│   ├──📂 Interfaces/
│   ├──📂 Mappers/
│   ├──📂 Services/
│──📂 FCG.Domain/
│   ├──📂 Entities/
│   ├──📂 Enums/
│   ├──📂 Exceptions/
│   ├──📂 Interfaces/
│──📂 FCG.Infrastructure/
│   ├──📂 Context/
│   ├──📂 Mappings/
│   ├──📂 Migrations/
│   ├──📂 Repositories/
│──📂 FCG.Tests/
│   ├──📂 Fixtures
│   ├──📂 IntegrationTests/
│   ├──📂 UnitTests/
```
#### 1. API Layer (Camada de API)
Gerencia a exposição dos serviços para consumo externo.
- Configurations: Organiza as configurações iniciais da aplicação como injeção de dependências, configurações iniciais e do swagger separando em classes distintas.
- Controllers: Definem os endpoints HTTP e lidam com requisições e respostas.
- Middleware: Responsável pelo tratamento de erros e logs estruturados.

#### 2. Application Layer (Camada de Aplicação)
Responsável por orquestrar a lógica de aplicação, fazendo a ponte entre a API e o domínio.
- DTOs: Objetos usados para transferir dados entre as camadas de aplicação sem expor diretamente as entidades do domínio.
- Interfaces: Definem contratos para serviços e interações, garantindo abstração e desacoplamento.
- Mappers: Responsáveis por converter objetos do domínio em DTOs e vice-versa.
- Services: Contêm lógica de aplicação, coordenando chamadas ao domínio e infraestrutura.

#### 3. Domain Layer (Camada de Domínio)
Representa o núcleo do sistema, contendo as abstrações.
- Entities: Modelos que representam objetos persistentes com suas regras de negócio.
- Enums: Listas de valores pré-definidos usados para categorização e organização.
- Exceptions: Lida com erros específicos do domínio.
- Interfaces: Definem contratos para repositórios, garantindo a abstração entre o domínio e a infraestrutura.

#### 4. Infrastructure Layer (Camada de Infraestrutura)
Responsável por interações externas, como banco de dados e serviços externos.
- Context: Implementa a conexão e configura\ção do banco de dados (exemplo: DbContext do Entity Framework).
- Repositories: Camada de persistência de dados que implementa os contratos definidos no domínio.

#### 5. Tests Layer (Camada de Testes)
Responsável por validar o funcionamento correto da aplicação, garantindo estabilidade e qualidade do software.
- Fixtures: Fornece objetos e configurações para testes automatizados.
- UnitTests: Testam funcionalidades isoladas, garantindo que métodos individuais se comportem conforme esperado.
- IntegrationTests: Validam a interação entre componentes e camadas do sistema, assegurando integração correta.

## 🏛️ Entidades do Domínio
A API gerencia as seguintes entidades:

### Usuário:
Representa um jogador, contendo informações como nome, email e senha criptografada.

### Jogo:
Contém detalhes como título, gênero e valor.

### Promoção:
Cupons de promoção para descontos na compra dos jogos.

### Pedido:
Registra compras de jogos com seus respectivos valores.

## ⚙️ Funcionalidades da Api
A API expõe os seguintes endpoints:

### Usuários

| **Método** | **Endpoint** | **Descrição** |
| ------ | ------ | ------ |
| 🟩 **POST** | `/Usuarios/Login`  | Efetua a autenticação do usuário retornando o seu token de acesso |
| 🔵 **GET** | `/Usuarios/ObterUsuario` | Obtém os dados de um determinado usuário pelo seu Id |
| 🔵 **GET** | `/Usuarios/ObterUsuarios` | Obtém uma lista com todos os usuários cadastrados |
| 🟩 **POST** | `/Usuarios/AdicionarUsuario` | Cria um novo cadastro de usuário |
| 🟧 **PUT**  | `/Usuarios/AlterarUsuario` | Altera os dados do usuário |
| 🟧 **PUT**  | `/Usuarios/AtivarrUsuario` | Ativa o usuário possibilitando a visualização dos seus dados e o seu acesso ao sistema |
| 🟧 **PUT**  | `/Usuarios/DesativarrUsuario` | Desativa o usuário impossibilitando a visualização dos seus dados e o seu acesso ao sistema |

### Jogos

| **Método** | **Endpoint** | **Descrição** |
| ------ | ------ | ------ |
| 🔵 **GET** | `/Jogos/ObterJogo` | Obtém os dados de um determinado jogo pelo seu Id |
| 🔵 **GET** | `/Jogos/ObterJogos` | Obtém uma lista com todos os jogos cadastrados |
| 🟩 **POST** | `/Jogos/AdicionarJogo` | Cria um novo cadastro de jogo |
| 🟧 **PUT**  | `/Jogos/AlterarUsuario` | Altera os dados do jogo |
| 🟧 **PUT**  | `/Jogos/AtivarJogo` | Ativa o jogo possibilitando a sua utilização no sistema |
| 🟧 **PUT**  | `/Jogos/DesativarJogo` | Desativa o jogo impossibilitando a sua utilização no sistema |


### Promoções

| **Método** | **Endpoint** | **Descrição** |
| ------ | ------ | ------ |
| 🔵 **GET** | `/Promocoes/ObterPromocao` | Obtém os dados de uma determinada promoção pelo seu Id |
| 🔵 **GET** | `/Promocoes/ObterPromocoes` | Obtém uma lista com todas as promoções cadastradas |
| 🟩 **POST** | `/Promocoes/AdicionarPromocao` | Cria um novo cadastro de promoção |
| 🟧 **PUT**  | `/Promocoes/AlterarPromocao` | Altera os dados da promoção |
| 🟧 **PUT**  | `/Promocoes/AtivarPromocao` | Ativa a promoção possibilitando a sua utilização no sistema |
| 🟧 **PUT**  | `/Promocoes/DesativarPromocao` | Desativa a promoção impossibilitando a sua utilização no sistema |

### Pedidos

| **Método** | **Endpoint** | **Descrição** |
| ------ | ------ | ------ |
| 🔵 **GET** | `/Pedidos/ObterPedido` | Obtém os dados de um determinado pedido pelo seu Id |
| 🔵 **GET** | `/Pedidos/ObterPedidos` | Obtém uma lista com todos os pedidos cadastrados |
| 🟩 **POST** | `/Pedidos/AdicionarPedido` | Cria um novo cadastro de pedido |
| 🟧 **PUT**  | `/Pedidos/AlterarPedido` | Altera os dados do pedido |
| 🟧 **PUT**  | `/Pedidos/AtivarPedido` | Ativa o pedido possibilitando a sua utilização no sistema |
| 🟧 **PUT**  | `/Pedidos/DesativarPedido` | Desativa o pedido impossibilitando a sua utilização no sistema |


## 🚀 Executando os testes

Para garantir a qualidade e a estabilidade do projeto, é essencial executar os testes automatizados. O projeto utiliza xUnit para testes e Moq para simulação de dependências.

### Estrutura dos testes
Os testes estão organizados conforme a estrutura do projeto:

```
FCG.Tests
│── 📂 UnitTests
│    │── 📄 JogoServiceTests.cs (Testes do serviço de jogos)
│    │── 📄 PedidoServiceTests.cs (Testes do serviço de pedido)
│    │── 📄 PromocaoServiceTests.cs (Testes do serviço de promoções)
│    │── 📄 UsuarioServiceTests.cs (Testes do serviço de usuários)
│── 📂 IntegrationTests
│    │── 📄 PedidoServiceTests.cs (Testes do serviço de pedidos)
```
Para rodar os testes, siga os passos:

#### ✅ Executar todos os testes
```
dotnet test
```

#### ✅ Executar um teste espesífico

```
dotnet test --filter FullyQualifiedName=Namespace.Classe.Teste
```

Exemplo:
```
dotnet test --filter FullyQualifiedName=FCG.Tests.IntegrationTests.ServicesTests.AdicionarPedido_ComDadosValidos_DeveSalvarNoBanco
```

#### ✅ Executar apenas testes unitários
```
dotnet test --filter Category=Unit
```

#### ✅ Executar apenas testes de integração
```
dotnet test --filter Category=Integration
```

## ✒️ Autor
*Márcio Henrique Vieira dos Santos - ✉️ marciohenriquev@gmail.com*

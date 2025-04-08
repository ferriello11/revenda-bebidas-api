
# 🍻 revenda-bebidas-api

API desenvolvida em .NET para gerenciamento de revendas de bebidas. A aplicação permite o cadastro e gerenciamento de clientes, produtos, lojas e pedidos, além de integrar com sistemas externos como a Ambev via fila SQS.

---

## 📦 Funcionalidades

- Cadastro e consulta de **clientes**, **lojas**, **produtos** e **pedidos**
- Integração com **fila SQS** da Ambev para consumo automático de pedidos
- Simulação de API externa da Ambev via **serviço mock**
- Estrutura preparada para escalar e integrar com serviços AWS

---

## 🚀 Como executar o projeto

### Pré-requisitos

- [.NET SDK 6+](https://dotnet.microsoft.com/download)
- Visual Studio 2022 ou VS Code com extensão C#
- (Opcional) AWS CLI configurada para acesso à fila SQS

### Execução local

```bash
cd src/Revenda
dotnet restore
dotnet run
```

A API será iniciada em: `https://localhost:5001`

---

## ⚙️ Estrutura do Projeto

```
revenda-bebidas-api/
│
├── src/
│   └── Revenda/
│       ├── Controllers/               # Endpoints REST
│       ├── Application/
│       │   ├── Services/              # Regras de negócio
│       │   └── JobIntegrationAmbve/   # Worker da fila SQS
│       ├── appsettings.json           # Configurações da aplicação
│       ├── Program.cs                 # Startup do app
│       └── Revenda.csproj             # Projeto C#
```

---

## 📌 Endpoints disponíveis

| Método | Rota                      | Descrição                   |
|--------|---------------------------|-----------------------------|
| GET    | /api/clientes             | Lista todos os clientes     |
| POST   | /api/clientes             | Cria um novo cliente        |
| GET    | /api/lojas                | Lista todas as lojas        |
| POST   | /api/lojas                | Cria uma nova loja          |
| GET    | /api/produtos             | Lista todos os produtos     |
| POST   | /api/produtos             | Cria um novo produto        |
| GET    | /api/pedidos              | Lista todos os pedidos      |
| POST   | /api/pedidos              | Cria um novo pedido         |

> **Observação:** os endpoints reais podem variar. Consulte os controllers para mais detalhes ou adicione Swagger para documentação automática.

---

📤 Entrega de pedidos à Ambev
Para garantir a resiliência no envio dos pedidos à Ambev, a aplicação utiliza uma arquitetura baseada em fila SQS (Amazon Simple Queue Service).

Como funciona
O pedido do cliente é recebido e validado pela API.

Em vez de enviar diretamente para a Ambev, o pedido é colocado na fila SQS.

Um worker (JobIntegrationAmbve) consome essa fila e realiza o envio à API da Ambev.

Se a API estiver indisponível, a mensagem permanece na fila até que o envio seja bem-sucedido.

Esse mecanismo garante tolerância a falhas, garantia de entrega e desacoplamento entre os serviços, evitando perda de dados em cenários de instabilidade externa.

---

## 🛠 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- AWS SQS (para consumo de mensagens)
- JSON como formato padrão de comunicação
- Arquitetura em camadas (Controller / Service / Integration)

---

## 💡 Possíveis melhorias

- Autenticação JWT
- Testes unitários com xUnit

---

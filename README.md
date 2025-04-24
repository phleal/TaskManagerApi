# 📋 TaskManagerApi

Uma API simples de gerenciamento de tarefas, construída com .NET 6, seguindo boas práticas de organização, documentação e padronização de respostas.

---

## ⚙️ Tecnologias Utilizadas

- .NET 8
- Swagger (Swashbuckle)
- Entity Framework Core
- C#

---

## 🧠 Funcionalidades Implementadas

### ✅ Criar uma nova tarefa

- **POST** `/api/tasks`
- Cria uma tarefa com título, descrição, data de vencimento, e vincula a um usuário.

### ✅ Listar tarefas por usuário

- **GET** `/api/tasks/{userId}`
- Retorna todas as tarefas relacionadas ao usuário informado.

### ✅ Marcar tarefa como concluída

- **PUT** `/api/tasks/{id}/complete`
- Marca a tarefa como concluída, caso ainda não esteja.

### ✅ Excluir tarefa

- **DELETE** `/api/tasks/{id}`
- Exclui uma tarefa pelo ID informado.

---


---

## 📄 Padrão de Respostas

A API utiliza a classe `BaseResponse` para unificar as respostas:
```json
{
  "success": true,
  "message": "Tarefa criada com sucesso",
  "data": {
    "id": "123",
    "title": "Estudar .NET",
    ...
  }
}
```

---

## 🧪 Documentação Swagger

- Acesse via navegador após rodar a aplicação:
```
https://localhost:{porta}/swagger
```
- Todas as rotas e modelos estão documentados com `[SwaggerOperation]`, `[SwaggerResponse]` e `[SwaggerSchema]`.

---

## 🚀 Como Rodar o Projeto Localmente

1. **Clone o repositório**
```bash
git clone https://github.com/phleal/TaskManagerApi.git
cd TaskManagerApi
```

2. **Configure o banco de dados no `appsettings.json`**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManagerDb;Trusted_Connection=True;"
}
```

3. **Crie o banco via migrations (caso use EF Core)**
```bash
dotnet ef database update
```

4. **Execute o projeto**
```bash
dotnet run
```

5. **Acesse no navegador**
```
https://localhost:{porta}/swagger
```


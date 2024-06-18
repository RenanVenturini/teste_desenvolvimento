# Documentação da API

## Visão Geral

Esta documentação cobre a API `UsuarioController` no projeto `AplicaçãoWebCompleta`. A API fornece endpoints para gerenciamento de usuários e consulta de informações de endereço pelo CEP com cache.

## Endpoints

### Criar Usuário

```markdown
- **Endpoint:** `POST /api/Usuario`
- **Descrição:** Cria um novo usuário.
- **Exemplo de Corpo da Requisição:**
  ```json
  {
    "nome": "Nome do Usuário",
    "email": "usuario@email.com",
    "senha": "senha123"
  }


### Atualizar Usuário

```markdown
- **Endpoint:** `PUT /api/Usuario`
- **Descrição:** Atualiza um usuário existente.
- **Exemplo de Corpo da Requisição:**
  ```json
  {
    "usuarioId": 1,
    "nome": "Novo Nome",
    "email": "novo_email@email.com",
    "senha": "nova_senha",
    "telefone": "987654321",
    "endereco": {
      "logradouro": "Nova Rua",
      "bairro": "Novo Bairro",
      "cidade": "Nova Cidade",
      "estado": "Novo Estado"
    }
  }


### Listar Usuários

```markdown
- **Endpoint:** `GET /api/Usuario`
- **Descrição:** Lista todos os usuários cadastrados.
- **Resposta de Sucesso:** `200 OK`
  ```json
  [
    {
      "usuarioId": 1,
      "nome": "Nome do Usuário",
      "email": "usuario@email.com",
      "telefone": "1234567890",
      "endereco": {
        "logradouro": "Rua Exemplo",
        "bairro": "Bairro Teste",
        "cidade": "Cidade",
        "estado": "Estado"
      }
    },
    {
      "usuarioId": 2,
      "nome": "Outro Usuário",
      "email": "outro@email.com",
      "telefone": "987654321",
      "endereco": {
        "logradouro": "Rua Outra",
        "bairro": "Outro Bairro",
        "cidade": "Outra Cidade",
        "estado": "Outro Estado"
      }
    }
  ]


### Buscar Usuário por ID

```markdown
- **Endpoint:** `GET /api/Usuario/{id}`
- **Descrição:** Recupera um usuário pelo seu ID.
- **Parâmetros:**
  - `id` (int): ID do usuário desejado.
- **Resposta de Sucesso:** `200 OK`
  ```json
  {
    "usuarioId": 1,
    "nome": "Nome do Usuário",
    "email": "usuario@email.com",
    "telefone": "1234567890",
    "endereco": {
      "logradouro": "Rua Exemplo",
      "bairro": "Bairro Teste",
      "cidade": "Cidade",
      "estado": "Estado"
    }
  }


### Remover Usuário

```markdown
- **Endpoint:** `DELETE /api/Usuario/{id}`
- **Descrição:** Remove um usuário pelo seu ID.
- **Parâmetros:**
  - `id` (int): ID do usuário a ser removido.
- **Resposta de Sucesso:** `204 No Content`
  - O usuário é removido com sucesso.

  ### Consultar Cep

- **Endpoint:** `GET /api/Usuario/consulta-cep/{cep}`
- **Descrição:** Consulta informações de endereço pelo CEP com cache.
- **Parâmetros:**
  - `cep` (string): CEP desejado.
- **Resposta de Sucesso:** `200 OK`
  ```json
  {
    "logradouro": "Rua Exemplo",
    "bairro": "Bairro Teste",
    "cidade": "Cidade",
    "estado": "Estado"
  }

  
## Alterando a String de Conexão

Para alterar a string de conexão usada pela aplicação, siga estes passos:

```markdown
1. Abra o arquivo `appsettings.json` no diretório raiz do seu projeto.
2. Localize a seção `ConnectionStrings`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=nome_servidor;Database=nome_banco;User Id=usuario;Password=senha;"
     }
   }


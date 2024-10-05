# Projeto WebApi Biblioteca em .NET - CRUD em Padrão MVC

Este projeto é uma WebAPI desenvolvida em .NET para fins de estudo de conceitos de infraestrutura e CRUD utilizando o padrão de arquitetura MVC.

## Tecnologias Utilizadas

- .NET
- ASP.NET Core
- Entity Framework Core

## Funcionalidades

1. **Criação de Livros**: Permite a criação de livros cadastrando as informações como autor e categoria no banco de dados.
2. **Criação de Autores**: Permite a criação de autores e relacionamento com a tabela Livros.
2. **Leitura de Recursos**: Permite a leitura de itens existentes no banco de dados.
3. **Atualização de Recursos**: Permite a atualização de itens existentes.
4. **Exclusão de Recursos**: Permite a exclusão de itens existentes.

## Como Rodar o Projeto

1. Clone o repositório:

    ```bash
    git clone https://github.com/luisfelipeprs/WebApi8-Library.git
    cd WebApi8-Library
    ```

2. Instale as dependências:

    ```bash
    dotnet restore
    ```

3. Configure a string de conexão com o banco de dados:

    ```bash
    "ConnectionStrings": 
    {
        "DefaultConnection": "server=localhost;Database=WebApiLibrary;Uid=root;Pwd=root"
    }
    ```

4. Rode o projeto:

    ```bash
    dotnet run
    ```

5. Aplique as migrações para o banco de dados:

    ```bash
    dotnet ef database update
    ```

## Estrutura do Projeto

- **Models**: Contém as classes de modelo que representam os dados do aplicativo.
- **Controllers**: Contém os controladores que lidam com as requisições HTTP e retornam as respostas.
- **Repositories**: Contém as classes responsáveis pela comunicação com o banco de dados.

## Endpoints de Autores

- `GET /api/Author/ListAuthors` - Retorna todos os autores.
- `GET /api/Author/AuthorById/{idAuthor}` - Retorna um autor específico por ID.
- `GET /api/Author/AuthorByIdBook/{idBook}` - Retorna um autor específico por ID do Livro.
- `POST /api/Author/CreateAuthor` - Cria um novo autor.
- `PUT /api/Author/{idAuthor}` - Atualiza um autor existente.
- `DELETE /api/Author/{idAuthor}` - Exclui um autor existente.

## Endpoints de Livros

- `GET /api/Book/ListBooks` - Retorna todos os livros.
- `GET /api/Book/BookById/{idBook}` - Retorna um livro específico por ID.
- `GET /api/Book/BooksByAuthor/{idAuthor}` - Retorna livros por ID do Autor.
- `POST /api/Book/CreateBook` - Cria um novo livro.
- `PUT /api/Book/UpdateBook/{idBook}` - Atualiza um livro existente.
- `DELETE /api/Book/DeleteBook/{idBook}` - Exclui um livro existente.

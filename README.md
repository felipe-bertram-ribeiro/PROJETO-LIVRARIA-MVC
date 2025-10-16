AUTOR: FELIPE BERTRAM RIBEIRO 
EMAIL PRA CONTATO: felipebertram3014@gmail.com

# Sistema de Gerenciamento de Livraria

## Descrição

Este projeto é um sistema web desenvolvido em **ASP.NET Core MVC** para gerenciar uma livraria. Permite o cadastro e controle de **clientes**, **funcionários**, **livros** e **empréstimos**, oferecendo uma interface intuitiva e eficiente para operações de rotina da livraria.

O sistema foi desenvolvido com foco em boas práticas de desenvolvimento web, arquitetura MVC, Entity Framework Core para persistência de dados e uma interface responsiva usando Bootstrap.

O projeto ainda não está concluido e virão novas adições.
---

## Funcionalidades

- **Dashboard inicial** com visão geral do sistema.
- **Clientes**
  - Listar, criar, editar, detalhar e excluir clientes.
  - Exibição de informações como nome, CPF, contato e e-mail.
- **Funcionários**
  - Listar, criar, editar, detalhar e excluir funcionários.
  - Controle de informações de contato e cargo.
- **Livros**
  - Gerenciamento completo de livros cadastrados.
- **Empréstimos**
  - Registro de empréstimos de livros para clientes.
  - Controle de datas de empréstimo, previsão de devolução e status.
  - Associação com cliente, funcionário e livro.

---

## Tecnologias Utilizadas

- **ASP.NET Core MVC**
- **Entity Framework Core** (com LocalDB)
- **C# 11**
- **Bootstrap 5**
- **Razor Views**
- **SQL Server (LocalDB)**

---

## Estrutura do Projeto

- **Controllers/**: Contém os controladores MVC do sistema.
- **Models/**: Define as entidades do sistema (Cliente, Funcionario, Livro, Emprestimo).
- **Views/**: Contém as páginas Razor do sistema organizadas por entidade.
- **wwwroot/**: Arquivos estáticos (CSS, JS, imagens).
- **Data/**: Contexto do Entity Framework Core para acesso ao banco de dados.


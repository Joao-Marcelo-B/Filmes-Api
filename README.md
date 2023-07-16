<h1 align="center"> Projeto de estudo Back-end - Api de Filmes </h1>

![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)<br>

## Descrição do Projeto
Esse projeto tem como objetivo concretizar os conhecimentos obtidos durante a minha formação feita na plataforna da <a href="https://www.alura.com.br/">Alura</a> sobre criação de **Apis Web** no padrão **REST com C#, Entity Framework e LINQ**.<br>
A Api de filmes construída possuí uma integração com um banco de dados MySQL e com ela é possível realizar um CRUD completo usando os verbos **GET, POST, PUT, PATCH e DELETE**. Além disso a aplicação possuí as seguintes entidades: filmes, cinemas, endereços e sessões construindo assim toda uma estrutura de banco relacional.

> :construction: Projeto em construção :construction:

# :hammer: Funcionalidades do projeto
- É possível fazer consultas, inserções, atualizações e deleções com as entidades (exceto com a entidade sessão que pode apenas inserir e consultar)
![Animação](https://github.com/Joao-Marcelo-B/Filmes-Api/assets/113398296/2ac8d966-feac-4be8-9bb2-a66eaec6cc79)
- Todas entidades possuem dois métodos para o verbo GET. O primeiro retorna todos os registros, além disso é possível definir um skip(parâmetro que defini quantos elementos pular) e um take(parâmetro para quantos elementos pegar), segue um exemplo de consulta: https://localhost:7221/filme?skip=5





<h1 align="center"> Projeto de estudo Back-end - Api de Filmes </h1>

![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)<br>

## Descrição do Projeto
Esse projeto tem como objetivo concretizar os conhecimentos obtidos durante a minha formação feita na plataforna da <a href="https://www.alura.com.br/">Alura</a> sobre criação de **Apis Web** no padrão **REST com C#, Entity Framework e LINQ**.<br>
A Api de filmes construída possuí uma integração com um banco de dados MySQL e com ela é possível realizar um CRUD completo usando os verbos **GET, POST, PUT, PATCH e DELETE**. Além disso a aplicação possuí as seguintes entidades: filmes, cinemas, endereços e sessões construindo assim toda uma estrutura de banco relacional.

> :construction: Projeto em construção :construction:

# :hammer: Funcionalidades do projeto
- É possível fazer consultas, inserções, atualizações e deleções com as entidades (exceto com a entidade sessão que pode apenas inserir e consultar)
<div style="max-width: 20px;max-height: 15px">

![Animação](https://github.com/Joao-Marcelo-B/Filmes-Api/assets/113398296/2ac8d966-feac-4be8-9bb2-a66eaec6cc79)

</div>
- Todas entidades possuem dois métodos de consulta para o verbo GET, o primeiro recupera mais de um objeto e o segundo recupera o objeto por Id
- Além disso o primerio método GET é possível definir um skip(parâmetro que define quantos elementos pular) e um take(parâmetro que define quantos elementos pegar), segue um exemplo de consulta: https://localhost:7221/Filme?skip=2&take=5, nesse caso ele não irá retornar o primerio e segundo filme mas começará a partir do terceiro e com o valor do take em 5 ele retornará do terceito filme até o sétimo.
<div align="center">
  
![Animação](https://github.com/Joao-Marcelo-B/Filmes-Api/assets/113398296/529490e3-7cb5-4140-b196-70faf3183754)

</div>

- Como você pode ter percebido no verbo GET da rota /Filme existe um terceiro parâmetro chamado "nomeCinema". Caso queria retornar os filmes que estão disponíveis em um determinado cinema basta digitar o nome do cinema na query que ele retornará apenas os filmes daquele mesmo cinema. Exemplo da url para chamada do método: https://localhost:7221/Filme?nomeCinema=Cine%20JM
<div align="center">

![exemplo_query](https://github.com/Joao-Marcelo-B/Filmes-Api/assets/113398296/2b761e91-1abb-441d-a4b2-4f01f49cb097)

</div>

> Esse parâmetro de query está disponivel apenas na rota de /Filme, as demais rotas possuem apenas os parâmetros de query **skip** e **take**



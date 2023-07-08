using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos de um Filme para serem validados e por fim adicionado ao banco</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdionarFilme([FromBody]CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.Id}, filme);
    }

    /// <summary>
    ///     Recupera os filmes do Banco de Dados de acordo com os parâmetro Skip e Take
    /// </summary>
    /// <param name="skip">Pula a quantidade de objetos informados</param>
    /// <param name="take">Pega a quantidade de objetos informados</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a busca seja realizada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery]int skip = 0, 
        [FromQuery]int take = 50, [FromQuery]string? nomeCinema = null)
    {
        if (nomeCinema == null)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
        }

        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
            .Skip(skip).Take(take).Where(filme => filme.Sessoes
            .Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
    }

    /// <summary>
    ///     Recupera o Filme pelo Id
    /// </summary>
    /// <param name="id">Id do Filme que deseja fazer a busca</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca por Id seja realizada com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);
    }

    /// <summary>
    ///     Atualiza todos os dados do Filme 
    /// </summary>
    /// <param name="id">Id do Filme a ser atualizado</param>
    /// <param name="filmeDto">Objeto usado para fazer validação e inserção dos dados atualizados</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja realizada com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaFilme(int id,
        [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if(filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    ///     Atualiza o Filme parcialmente sem precisar se atualizar todo o Objeto
    /// </summary>
    /// <param name="id">Id do Filme a ser atualizado</param>
    /// <param name="patch">Parte a ser atualizada do Objeto</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja realizada com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaFilmeParcial(int id, 
        JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if(!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar , filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    ///     Deleta o Filme do Banco de Dados
    /// </summary>
    /// <param name="id">Id do Filme a ser deletado</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a deleção seja realizada com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}

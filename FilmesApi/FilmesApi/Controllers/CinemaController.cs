using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaCinema([FromBody]CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);         //Essa função transforma um CinemaDto em um Cinema
        _context.Cinemas.Add(cinema);                           //Esse método adiciona no banco de dados
        _context.SaveChanges();                                 //Esse método concretiza a ação no Banco
        return CreatedAtAction(nameof(RecuperaCinemaPorId), new {id = cinema.Id}, cinema);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> RecuperaCinemas()
    {
        return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCinemaPorId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id); //Esse método faz a busca dentro do banco

        if (cinema == null) return NotFound();

        var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

        return Ok(cinemaDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if(cinema == null) return NotFound();

        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaCinemaParcial(int id, JsonPatchDocument<UpdateCinemaDto> patch)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null) return NotFound();

        var cinemaParaAtualizar = _mapper.Map<UpdateCinemaDto>(cinema);

        patch.ApplyTo(cinemaParaAtualizar, ModelState);

        if (!TryValidateModel(cinemaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(cinemaParaAtualizar, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if(cinema == null) return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();

        return NoContent();
    }
}

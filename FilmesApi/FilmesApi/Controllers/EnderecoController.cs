using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(IMapper mapper, FilmeContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaEndereco([FromBody]CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

        _context.Enderecos.Add(endereco);
        _context.SaveChanges();

        return CreatedAtAction(nameof(RecuperaEnderecoPorId), new {id = endereco.Id}, endereco);
    }

    [HttpGet]
    public IEnumerable<ReadEnderecoDto> RecuperaEnderecos()
    {
        return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaEnderecoPorId(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

        return Ok(enderecoDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaEndereco(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        UpdateEnderecoDto enderecoDto = _mapper.Map<UpdateEnderecoDto>(endereco);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaEnderecoParcial(int id, 
        [FromBody] JsonPatchDocument<UpdateEnderecoDto> patch)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        var enderecoParaAtualizar = _mapper.Map<UpdateEnderecoDto>(endereco);

        patch.ApplyTo(enderecoParaAtualizar, ModelState);

        if (!TryValidateModel(enderecoParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(enderecoParaAtualizar, endereco);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaEndereco(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        _context.Remove(endereco);
        _context.SaveChanges();

        return NoContent();
    }
}

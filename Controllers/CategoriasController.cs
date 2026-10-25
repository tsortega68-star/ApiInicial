using ApiInicial.DTOs;
using ApiInicial.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInicial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaResponseDto>>> Get()
    {
        var categorias = await _categoriaService.ObtenerTodasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponseDto>> GetById(int id)
    {
        var categoria = await _categoriaService.ObtenerPorIdAsync(id);
        if (categoria == null) return NotFound();
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> Post(CategoriaRequestDto categoriaDto)
    {
        var nuevaCategoria = await _categoriaService.CrearAsync(categoriaDto);
        return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Id }, nuevaCategoria);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoriaResponseDto>> Put(int id, CategoriaRequestDto categoriaDto)
    {
        var categoriaActualizada = await _categoriaService.ActualizarAsync(id, categoriaDto);
        if (categoriaActualizada == null) return NotFound();
        return Ok(categoriaActualizada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await _categoriaService.EliminarAsync(id);
        if (!resultado) return NotFound();
        return NoContent();
    }
}
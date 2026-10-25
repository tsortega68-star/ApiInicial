using ApiInicial.DTOs;
using ApiInicial.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInicial.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoResponseDto>>> ObtenerTodos()
    {
        var productos = await _productoService.ObtenerTodosAsync();
        return Ok(productos);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoResponseDto>> Crear([FromBody] ProductoRequestDto productoRequest)
    {
        var nuevoProducto = await _productoService.CrearProductoAsync(productoRequest);
        return Ok(nuevoProducto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductoResponseDto>> Actualizar(int id, [FromBody] ProductoRequestDto productoRequest)
    {
        var resultado = await _productoService.ActualizarProducto(id, productoRequest);
        if (resultado == null)
        {
            return NotFound();
        }
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _productoService.EliminarAsync(id);
        if (!eliminado)
        {
            return NotFound();
        }
        return NoContent();
    }
}
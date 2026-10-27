using Microsoft.AspNetCore.Mvc;
using ApiInicial.DTOs;
using ApiInicial.Services;

namespace ApiInicial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoRequestDto productoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoProducto = await _productoService.CrearProductoAsync(productoRequest);
            return StatusCode(201, nuevoProducto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoRequestDto producto)
        {
            var productoActualizado = await _productoService.ActualizarProducto(id, producto);
            if (productoActualizado == null)
            {
                return NotFound(new { mensaje = "Producto no encontrado para actualizar" });
            }
            return Ok(productoActualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _productoService.EliminarAsync(id);
            if (!eliminado)
            {
                return NotFound(new { mensaje = "No se pudo eliminar, producto no encontrado" });
            }
            return NoContent();
        }
    }
}
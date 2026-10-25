using ApiInicial.DTOs;
using ApiInicial.Models;

namespace ApiInicial.Services;
public interface IProductoService
{
    Task<List<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto productoRequest);
    Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto);
    Task<bool> EliminarAsync(int id);
}
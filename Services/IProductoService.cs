using ApiInicial.DTOs;
using ApiInicial.Models;

namespace ApiInicial.Services;
public interface IProductoService
{
    Task<List<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto?> ObtenerPorIdAsync(int id);
    Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto productoRequest);
    Task<ProductoResponseDto?> ActualizarProducto(int id, ProductoRequestDto producto); // 👈 Agrega el '?' aquí
    Task<bool> EliminarAsync(int id);
}
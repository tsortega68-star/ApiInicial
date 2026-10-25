using ApiInicial.DTOs;

namespace ApiInicial.Services;

public interface ICategoriaService
{
    Task<List<CategoriaResponseDto>> ObtenerTodasAsync();
    Task<CategoriaResponseDto?> ObtenerPorIdAsync(int id);
    Task<CategoriaResponseDto> CrearAsync(CategoriaRequestDto categoriaDto);
    Task<CategoriaResponseDto?> ActualizarAsync(int id, CategoriaRequestDto categoriaDto);
    Task<bool> EliminarAsync(int id);
}
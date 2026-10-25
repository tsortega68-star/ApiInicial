using ApiInicial.Data;
using ApiInicial.DTOs;
using ApiInicial.Models;
using ApiInicial.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiInicial.Services;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaResponseDto>> ObtenerTodasAsync()
    {
        return await _context.Categorias
            .Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                Estado = c.Estado
            })
            .ToListAsync();
    }

    public async Task<CategoriaResponseDto?> ObtenerPorIdAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return null;

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Descripcion = categoria.Descripcion,
            Estado = categoria.Estado
        };
    }

    public async Task<CategoriaResponseDto> CrearAsync(CategoriaRequestDto categoriaDto)
    {
        var nuevaCategoria = new Categoria
        {
            Descripcion = categoriaDto.Descripcion,
            Estado = categoriaDto.Estado
        };

        _context.Categorias.Add(nuevaCategoria);
        await _context.SaveChangesAsync();

        return new CategoriaResponseDto
        {
            Id = nuevaCategoria.Id,
            Descripcion = nuevaCategoria.Descripcion,
            Estado = nuevaCategoria.Estado
        };
    }

    public async Task<CategoriaResponseDto?> ActualizarAsync(int id, CategoriaRequestDto categoriaDto)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return null;

        categoria.Descripcion = categoriaDto.Descripcion;
        categoria.Estado = categoriaDto.Estado;

        await _context.SaveChangesAsync();

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Descripcion = categoria.Descripcion,
            Estado = categoria.Estado
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}
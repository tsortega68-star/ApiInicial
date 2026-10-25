using ApiInicial.Data;
using ApiInicial.DTOs;
using ApiInicial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInicial.Services;

public class ProductoService : IProductoService
{
    private readonly AppDbContext _context;

    public ProductoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto)
    { 
        var existe = await _context.Productos.Include(p => p.Categoria).FirstAsync(p => p.Id ==id);
        if (existe == null)
            return null;

        existe.Nombre = producto.Nombre;
        existe.Precio = producto.Precio;
        existe.Stock = producto.Stock;
        existe.CategoriaId = producto.CategoriaId;

        await _context.SaveChangesAsync();
        return new ProductoResponseDto()
        {
            Id = existe.Id,
            Nombre = existe.Nombre,
            Precio = existe.Precio,
            Stock = existe.Stock,
            Estado = existe.Estado,
            CategoriaId = existe.CategoriaId,
            NombreCategoria = existe.Categoria?.Descripcion ?? string.Empty

        };
    }

    public async Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto productoRequest)
    {
        var nuevoProducto = new Producto
        {
            Nombre = productoRequest.Nombre,
            Precio = productoRequest.Precio,
            Stock = productoRequest.Stock,
            Estado = true,
            CategoriaId = productoRequest.CategoriaId // Corregido el error de tipeo
        };

        var prod = _context.Productos.Add(nuevoProducto);
        await _context.SaveChangesAsync();

        // Recargamos el producto incluyendo la categoría para obtener la descripción
        var productoCreado = await _context.Productos
            .Include(p => p.Categoria)
            .FirstAsync(p => p.Id == prod.Entity.Id);

        return new ProductoResponseDto()
        {
            Id = productoCreado.Id,
            Nombre = productoCreado.Nombre,
            Precio = productoCreado.Precio,
            Stock = productoCreado.Stock,
            Estado = productoCreado.Estado,
            CategoriaId = productoCreado.CategoriaId,
            NombreCategoria = productoCreado.Categoria?.Descripcion ?? string.Empty
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return false;
        }
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ProductoResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Productos
            .Include(p => p.Categoria)
            .Select(p => new ProductoResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                Estado = p.Estado,
                CategoriaId = p.CategoriaId,
                NombreCategoria = p.Categoria != null ? p.Categoria.Descripcion : string.Empty
            })
            .ToListAsync();
    }
}
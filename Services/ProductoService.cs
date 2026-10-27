using ApiInicial.Data;
using ApiInicial.DTOs;
using ApiInicial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInicial.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoResponseDto>> ObtenerTodosAsync()
        {
            var productos = await _context.Productos.ToListAsync();

            return productos.Select(p => new ProductoResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre ?? string.Empty,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId
            }).ToList();
        }

        public async Task<ProductoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null) return null;

            return new ProductoResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre ?? string.Empty,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId
            };
        }

        public async Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto productoRequest)
        {
            var producto = new Producto
            {
                Nombre = productoRequest.Nombre,
                Precio = productoRequest.Precio,
                Stock = productoRequest.Stock,
                CategoriaId = productoRequest.CategoriaId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return new ProductoResponseDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre ?? string.Empty,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId
            };
        }

        public async Task<ProductoResponseDto?> ActualizarProducto(int id, ProductoRequestDto productoRequest)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return null;

            producto.Nombre = productoRequest.Nombre;
            producto.Precio = productoRequest.Precio;
            producto.Stock = productoRequest.Stock;
            producto.CategoriaId = productoRequest.CategoriaId;

            await _context.SaveChangesAsync();

            return new ProductoResponseDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre ?? string.Empty,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId
            };
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
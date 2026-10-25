using System.ComponentModel.DataAnnotations;
using ApiInicial.DTOs;

namespace ApiInicial.DTOs;
public class ProductoRequestDto
{
    [Required]
    [StringLength(2, ErrorMessage = "La propiedad no tiene la longitud correcta")]

    public string Nombre { get; set; } = string.Empty;
     [Required]
     [Range(1, 100, ErrorMessage = "El precio es incorrecto")]
    public decimal Precio { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El stock tiene una cantidad negativa o cero")]
    public int Stock { get; set; }
    public bool Estado { get; set; }
    public int CategoriaId { get; set; }
}
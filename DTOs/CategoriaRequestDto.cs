using System.ComponentModel.DataAnnotations;

namespace ApiInicial.DTOs;

public class CategoriaRequestDto
{
    [Required]
    [StringLength(50, ErrorMessage = "La propiedad no tiene la longitud correcta")]
    public string Descripcion { get; set; } = string.Empty;

    public bool Estado { get; set; }
}
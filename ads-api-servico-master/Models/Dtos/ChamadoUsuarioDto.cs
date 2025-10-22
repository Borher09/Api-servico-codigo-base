using System.ComponentModel.DataAnnotations;

namespace ApiServico.Models.Dtos
{
    public class ChamadoUsuarioDto
    {
        [Required]
        [MinLength(1)]
        public List<int> UsuariosIds { get; set; } = new List<int>();
    }
}

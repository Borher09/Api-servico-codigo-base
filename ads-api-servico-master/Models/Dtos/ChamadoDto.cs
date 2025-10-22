using System.ComponentModel.DataAnnotations;

namespace ApiServico.Models.Dtos
{
    public class ChamadoDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        //[MinLength(10)]
        [Length(10, 100, ErrorMessage = "O titulo deve ter no mínimo 10 e no máximo 100 caracteres.")]
        public required string Titulo { get; set; }

        [Required]
        public required string Descricao { get; set; }

        [Required]
        public int PrioridadeId { get; set; }
    }
}

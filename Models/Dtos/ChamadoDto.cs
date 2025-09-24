using System.ComponentModel.DataAnnotations;

namespace aula2ApiServico.Models.Dtos
{
    public class ChamadoDto
    {
        [Required(ErrorMessage = "O titulo é obrigatorio")]
        // [MinLength(10)]
        [Length(10, 100, ErrorMessage = "O titulo deve ter no minimo 10 e no máximo 100 caracteres")]
        public required string titulo { get; set; }

        public required string Descricao { get; set; }


    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiServico.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id_usu")]
        public int Id { get; set; }

        [Column("nome_usu")]
        public string? Nome { get; set; }

        [Column("email_usu")]
        public string? Email { get; set; }

        [JsonIgnore]
        [Column("senha_usu")]
        public string? Senha { get; set; }

        public ICollection<Chamado> Chamados { get; set; } = [];
    }
}

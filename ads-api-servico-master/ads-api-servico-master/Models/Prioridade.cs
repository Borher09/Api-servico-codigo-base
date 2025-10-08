using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace ApiServico.Models
{
    [Table("prioridade")]
    public class Prioridade
    {
        [Column("id_pri")]
        public int? Id { get; set; }

        [Column("nome_pri")]
        public string? Nome { get; set; }
        /*
         * //Configuração d Realacionamento um-para-muitos
        //entre chamado (n) e prioridade (1)

         * Configuração inversa
         
         */
        [JsonIgnore]
        public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}

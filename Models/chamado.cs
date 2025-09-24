using aula2ApiServico.Models.Dtos;
using System.ComponentModel.DataAnnotations.Schema;


namespace aula2ApiServico.Models
{
    [Table("chamados")]
    public class Chamado
    {
        [Column("id_cha")]
        public int Id { get; set; }

        [Column("titulo_cha")]
        public required string titulo { get; set; }

        [Column("descricao_cha")]
        public required string Descricao { get; set; }

        [Column("data_abertura_cha")]
        public DateTime dateAbertura { get; set; }

        [Column("data_fechamento_cha")]
        public DateTime dataFechamento { get; set; }


        [Column("situacao")]
        public string situacao { get; set; } = "Aberto";

    }
}

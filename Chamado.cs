namespace aula2ApiServico
{
    public class Chamado
    {
        public int Id { get; set; }

        public required string Titulo { get; set; }
        public required string Descricao { get; set; }

        public string situacao { get; set; } = "Aberto";

    }
}

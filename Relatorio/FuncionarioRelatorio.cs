namespace SalaoDeCabelereiro.Relatorio
{
    class FuncionarioRelatorio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Profissao { get; set; }

        public FuncionarioRelatorio(int id, string nome, string profissao)
        {
            Id = id;
            Nome = nome;
            Profissao = profissao;
        }
    }
}

namespace SalaoDeCabelereiro.Relatorio
{
    class ProdutoRelatorio
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public double Peso { get; set; }
        public string Medicao { get; set; }
        public int Quantidade { get; set; }

        public ProdutoRelatorio(int id, string nome, double peso, string medicao, int quantidade)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Medicao = medicao;
            Quantidade = quantidade;
        }
    }
}

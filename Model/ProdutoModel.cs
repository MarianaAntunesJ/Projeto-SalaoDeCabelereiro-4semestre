namespace SalaoDeCabelereiro.Model
{
    class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Peso { get; set; }
        public string Medicao { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }

        public ProdutoModel()
        {
        }

        public ProdutoModel(int id, string nome, double peso, string medicao, int quantidade, bool ativo)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Medicao = medicao;
            Quantidade = quantidade;
            Ativo = ativo;
        }
    }
}

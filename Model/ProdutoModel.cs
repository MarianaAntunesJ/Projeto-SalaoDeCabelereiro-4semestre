namespace SalaoDeCabelereiro.Model
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double? Peso { get; set; }
        public string Medicao { get; set; }
        public int? Quantidade { get; set; }
        public bool Ativo { get; set; }
        public byte[] Imagem { get; set; } = new byte[1];

        public ProdutoModel()
        {
        }

        public ProdutoModel(int id, string nome, double peso, string medicao, int quantidade, bool ativo, byte[] imagem)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Medicao = medicao;
            Quantidade = quantidade;
            Ativo = ativo;
            Imagem = imagem;
        }
    }
}

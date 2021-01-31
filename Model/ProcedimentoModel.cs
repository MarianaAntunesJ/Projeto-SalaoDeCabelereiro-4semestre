using System.Collections.Generic;

namespace SalaoDeCabelereiro.Model
{
    class ProcedimentoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public string AreaProfissional { get; set; }
        public bool Ativo { get; set; }
        public List<ProdutoModel> Produtos { get; set; }

        public ProcedimentoModel(int id, string nome, int duracao, string areaProfissional, bool ativo, List<ProdutoModel> produtos)
        {
            Id = id;
            Nome = nome;
            Duracao = duracao;
            AreaProfissional = areaProfissional;
            Ativo = ativo;
            Produtos = produtos;
        }
    }
}

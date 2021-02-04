using System.Collections.Generic;

namespace SalaoDeCabelereiro.Model
{
    class ProcedimentoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Duracao { get; set; } //ToDo = arrumar tipo de duração int => double
        public string AreaProfissional { get; set; }
        public List<ProdutoModel> Produtos { get; set; }
        public bool Ativo { get; set; }

        public ProcedimentoModel()
        {
        }

        public ProcedimentoModel(int id, string nome, double duracao, string areaProfissional, List<ProdutoModel> produtos, bool ativo)
        {
            Id = id;
            Nome = nome;
            Duracao = duracao;
            AreaProfissional = areaProfissional;
            Produtos = produtos;
            Ativo = ativo;            
        }
    }
}

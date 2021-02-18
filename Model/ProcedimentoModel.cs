using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SalaoDeCabelereiro.Model
{
    public class ProcedimentoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string AreaProfissional { get; set; }
        public ObservableCollection<ProdutoModel> Produtos { get; set; }
        public bool Ativo { get; set; }

        public ProcedimentoModel()
        {
        }

        public ProcedimentoModel(int id, string nome, string areaProfissional, bool ativo)
        {
            Id = id;
            Nome = nome;
            AreaProfissional = areaProfissional;
            Ativo = ativo;
        }

        public ProcedimentoModel(int id, string nome, string areaProfissional, ObservableCollection<ProdutoModel> produtos, bool ativo) :
            this(id, nome, areaProfissional, ativo)
        {
            Produtos = produtos;
        }
    }
}

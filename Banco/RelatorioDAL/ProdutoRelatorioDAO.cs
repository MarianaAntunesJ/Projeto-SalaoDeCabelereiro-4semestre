using SalaoDeCabelereiro.Relatorio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco.RelatorioDAL
{
    class ProdutoRelatorioDAO
    {

        private readonly string _tabela = "Produto";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ProdutoRelatorioDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private List<ProdutoRelatorio> GetProdutos()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ProdutoRelatorio> produtos = new List<ProdutoRelatorio>();

            while (rd.Read())
            {
                ProdutoRelatorio produto = new ProdutoRelatorio(
                        (int)rd[nameof(ProdutoRelatorio.Id)],
                        (string)rd[nameof(ProdutoRelatorio.Nome)],
                        (double)rd[nameof(ProdutoRelatorio.Peso)],
                        (string)rd[nameof(ProdutoRelatorio.Medicao)],
                        (int)rd[nameof(ProdutoRelatorio.Quantidade)]);

                produtos.Add(produto);
            }
            rd.Close();
            return produtos;
        }

        public List<ProdutoRelatorio> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetProdutos();
        }
    }
}

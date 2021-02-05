using SalaoDeCabelereiro.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco
{
    class ProcedimentoDAO
    {

        private readonly string _tabela = "Procedimento";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ProcedimentoDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao() //ToDo: nome ideal para esta função
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosProcedimento(ProcedimentoModel procedimento)
        {
            GetConexao();
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Nome", procedimento.Nome);
            Cmd.Parameters.AddWithValue("@Duracao", procedimento.Duracao);
            Cmd.Parameters.AddWithValue("@AreaProfissional", procedimento.AreaProfissional);
            Cmd.Parameters.AddWithValue("@Produtos", procedimento.Produtos);
            Cmd.Parameters.AddWithValue("@Ativo", true);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Inserir(ProcedimentoModel produto)
        {
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Duracao, @Areaprofissional, @Produtos, @Ativo)";
            return DadosProcedimento(produto);
        }

        private List<ProcedimentoModel> GetProduto()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ProcedimentoModel> procedimentos = new List<ProcedimentoModel>();

            while (rd.Read())
            {
                ProcedimentoModel procedimento = new ProcedimentoModel(
                        (int)rd[nameof(ProcedimentoModel.Id)],
                        (string)rd[nameof(ProcedimentoModel.Nome)],
                        (int)rd[nameof(ProcedimentoModel.Duracao)],
                        (string)rd[nameof(ProcedimentoModel.AreaProfissional)],
                        (List<ProdutoModel>)rd[nameof(ProcedimentoModel.Produtos)],
                        (bool)rd[nameof(ProcedimentoModel.Ativo)]);

                procedimentos.Add(procedimento);
            }
            rd.Close();
            return procedimentos;
        }

        public List<ProcedimentoModel> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetProduto();
        }

        public List<ProcedimentoModel> Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE Id LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            return GetProduto();
        }

        public bool Atualizar(ProcedimentoModel procedimento)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Nome = @Nome, Duracao = @Duracao, AreaProfissional = @AreaProfissional, Produtos = @Produtos, Ativo = @Ativo  WHERE Id = @id";

            return DadosProcedimento(procedimento);
        }
    }
}

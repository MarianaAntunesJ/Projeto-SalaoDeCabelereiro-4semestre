using SalaoDeCabelereiro.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

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
            Cmd.Parameters.AddWithValue("@AreaProfissional", procedimento.AreaProfissional);
            Cmd.Parameters.AddWithValue("@Ativo", true);

            int modified = (int)Cmd.ExecuteScalar();
            if (modified != 0)
                return InsereProdutosDeProcedimento(modified, procedimento);
            return false;
        }

        private bool InsereProdutosDeProcedimento(int idProcedimento, ProcedimentoModel procedimento)
        {
            var sb = new StringBuilder();

            foreach (var produto in procedimento.Produtos)
                sb.Append($"({produto.Id}, {idProcedimento})\t");

            return InserirProdutosDeProcedimento(sb.ToString());
        }

        private bool InserirProdutosDeProcedimento(string valores)
        {
            GetConexao();
            Cmd.CommandText = $"INSERT INTO Produtos_De_Procedimento VALUES {valores}";
            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            return false;
        }

        public bool Inserir(ProcedimentoModel procedimento)
        {
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Areaprofissional, @Ativo)";
            return DadosProcedimento(procedimento);
        }

        private List<ProcedimentoModel> GetProcedimento()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ProcedimentoModel> procedimentos = new List<ProcedimentoModel>();

            while (rd.Read())
            {
                ProcedimentoModel procedimento = new ProcedimentoModel(
                        (int)rd[nameof(ProcedimentoModel.Id)],
                        (string)rd[nameof(ProcedimentoModel.Nome)],
                        (string)rd[nameof(ProcedimentoModel.AreaProfissional)],
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

            return GetProcedimento();
        }

        public List<ProcedimentoModel> Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE Id LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            return GetProcedimento();
        }

        public bool Atualizar(ProcedimentoModel procedimento)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Nome = @Nome, AreaProfissional = @AreaProfissional, Produtos = @Produtos, Ativo = @Ativo  WHERE Id = @id";

            return DadosProcedimento(procedimento);
        }
    }
}

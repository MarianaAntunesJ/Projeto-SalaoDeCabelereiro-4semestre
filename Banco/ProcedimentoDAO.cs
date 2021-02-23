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
        private ProdutoDAO _produtoDAO = new ProdutoDAO();

        public ProcedimentoDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosProcedimento(ProcedimentoModel procedimento)
        {            
            Cmd.Parameters.AddWithValue("@Nome", procedimento.Nome);
            Cmd.Parameters.AddWithValue("@AreaProfissional", procedimento.AreaProfissional);
            Cmd.Parameters.AddWithValue("@Ativo", procedimento.Ativo);

            int modified = (int)Cmd.ExecuteScalar();
            if (modified != 0)
                return InsereProdutosDeProcedimento(modified, procedimento);
            return false;
        }

        private bool DadosProcedimento1(ProcedimentoModel procedimento)
        {
            Cmd.Parameters.AddWithValue("@Nome", procedimento.Nome);
            Cmd.Parameters.AddWithValue("@AreaProfissional", procedimento.AreaProfissional);
            Cmd.Parameters.AddWithValue("@Ativo", procedimento.Ativo);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;

        }

            private bool InsereProdutosDeProcedimento(int idProcedimento, ProcedimentoModel procedimento)
        {
            var sb = new StringBuilder();

            foreach (var produto in procedimento.Produtos)
                sb.Append($"({produto.Id}, {idProcedimento}),\t");

            string valores = sb.ToString();
            valores = valores.Remove(valores.Length - 2);
            return InserirProdutosDeProcedimento(valores);
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
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Areaprofissional, @Ativo)";

            Cmd.Parameters.Clear();
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
            var ver = new ObservableCollection<ProdutoModel>();
            foreach (ProcedimentoModel item in procedimentos)
            {
                ver = new ObservableCollection<ProdutoModel>(GetProdutosDeProcedimento(item.Id));
                item.Produtos = ver;
            }
            return procedimentos;
        }

        private List<ProdutoModel> GetProdutosDeProcedimento(int idProcedimento)
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom("Produtos_De_Procedimento")} WHERE ProcedimentoId = @idProcedimento ";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@idProcedimento", idProcedimento);

            SqlDataReader rd = Cmd.ExecuteReader();
            List<int> produtos = new List<int>();

            while (rd.Read())
            {
                produtos.Add((int)rd["ProdutoId"]);
            }
            rd.Close();
            return ListaProdutosEscolhidos(produtos); 
        }

        private List<ProdutoModel> ListaProdutosEscolhidos(List<int> idProdutos)
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();
            foreach (int item in idProdutos)
               produtos.Add(_produtoDAO.GetProduto(item));
            return produtos;
        }

        public List<ProcedimentoModel> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";
            var a = GetProcedimento();

            return a;
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
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Nome = @Nome, AreaProfissional = @AreaProfissional, Ativo = @Ativo  WHERE Id = @id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", procedimento.Id);
            return DadosProcedimento1(procedimento);
        }
    }
}

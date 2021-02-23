using SalaoDeCabelereiro.Relatorio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco.RelatorioDAL
{
    class FuncionarioRelatorioDAO
    {
        private readonly string _tabela = "Funcionario";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public FuncionarioRelatorioDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private List<FuncionarioRelatorio> GetFuncionario()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<FuncionarioRelatorio> funcionarios = new List<FuncionarioRelatorio>();

            while (rd.Read())
            {
                FuncionarioRelatorio funcionario = new FuncionarioRelatorio(
                        (int)rd[nameof(FuncionarioRelatorio.Id)],
                        (string)rd[nameof(FuncionarioRelatorio.Nome)],
                        (string)rd[nameof(FuncionarioRelatorio.Profissao)]);

                funcionarios.Add(funcionario);
            }
            rd.Close();
            return funcionarios;
        }

        public List<FuncionarioRelatorio> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetFuncionario();
        }
    }
}

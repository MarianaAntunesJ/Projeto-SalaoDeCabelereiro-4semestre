using SalaoDeCabelereiro.Relatorio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco.RelatorioDAL
{
    class ProcedimentoRelatorioDAO
    {
        private readonly string _tabela = "Procedimento";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ProcedimentoRelatorioDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private List<ProcedimentoRelatorio> GetProcedimento()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ProcedimentoRelatorio> procedimentos = new List<ProcedimentoRelatorio>();

            while (rd.Read())
            {
                ProcedimentoRelatorio procedimento = new ProcedimentoRelatorio(
                        (int)rd[nameof(ProcedimentoRelatorio.Id)],
                        (string)rd[nameof(ProcedimentoRelatorio.Nome)],
                        (string)rd[nameof(ProcedimentoRelatorio.AreaProfissional)]);

                procedimentos.Add(procedimento);
            }
            rd.Close();
            return procedimentos;
        }

        public List<ProcedimentoRelatorio> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";
            var a = GetProcedimento();

            return a;
        }
    }
}

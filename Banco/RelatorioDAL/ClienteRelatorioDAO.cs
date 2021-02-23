using SalaoDeCabelereiro.Relatorio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco.RelatorioDAL
{
    class ClienteRelatorioDAO
    {
        private readonly string _tabela = "Cliente";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ClienteRelatorioDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private List<ClienteRelatorio> GetCliente()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ClienteRelatorio> clientes = new List<ClienteRelatorio>();

            while (rd.Read())
            {
                ClienteRelatorio cliente = new ClienteRelatorio(
                        (int)rd[nameof(ClienteRelatorio.Id)],
                        (string)rd[nameof(ClienteRelatorio.Nome)],
                        (string)rd[nameof(ClienteRelatorio.Telefone)]);

                clientes.Add(cliente);
            }
            rd.Close();
            return clientes;
        }

        public List<ClienteRelatorio> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetCliente();
        }
    }
}

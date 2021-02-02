using System.Data;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco
{
    class Conexao
    {
        private SqlConnection _conexao;

        public Conexao()
        {
            string cadeiaConexao = @"Data Source=DESKTOP-EBARGUA\SQLEXPRESS;Initial Catalog=SalaoDeBeleza;Integrated Security=True";
            _conexao = new SqlConnection(cadeiaConexao);
            _conexao.Open();
        }

        public void Desconectar()
        {
            if (_conexao.State == ConnectionState.Open)
            {
                _conexao.Close();
            }
        }

        public SqlConnection RetornarConexao()
        {
            return _conexao;
        }
    }
}
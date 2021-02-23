using SalaoDeCabelereiro.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace SalaoDeCabelereiro.Banco
{
    class FuncionarioDAO
    {
        private readonly string _tabela = "Funcionario";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public FuncionarioDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosFuncionario(FuncionarioModel funcionario)
        {
            Cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            Cmd.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
            Cmd.Parameters.AddWithValue("@CPF", funcionario.CPF);
            Cmd.Parameters.AddWithValue("@Profissao", funcionario.Profissao);
            Cmd.Parameters.AddWithValue("@Email", funcionario.Email);
            Cmd.Parameters.AddWithValue("@Ativo", true);
            Cmd.Parameters.AddWithValue("@Usuario", funcionario.Usuario);
            Cmd.Parameters.AddWithValue("@Senha", funcionario.Senha);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Inserir(FuncionarioModel funcionario)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Telefone, @CPF, @Profissao, @Email, @Ativo, @Usuario, @Senha)";
            Cmd.Parameters.Clear();
            return DadosFuncionario(funcionario);
        }

        private List<FuncionarioModel> GetFuncionario()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

            while (rd.Read())
            {
                FuncionarioModel funcionario = new FuncionarioModel(
                        (int)rd[nameof(FuncionarioModel.Id)],
                        (string)rd[nameof(FuncionarioModel.Nome)],
                        (string)rd[nameof(FuncionarioModel.Telefone)],
                        (string)rd[nameof(FuncionarioModel.CPF)],
                        (string)rd[nameof(FuncionarioModel.Profissao)],
                        (string)rd[nameof(FuncionarioModel.Email)],
                        (bool)rd[nameof(FuncionarioModel.Ativo)],
                        (string)rd[nameof(FuncionarioModel.Usuario)],
                        (string)rd[nameof(FuncionarioModel.Senha)]);

                funcionarios.Add(funcionario);
            }
            rd.Close();
            return funcionarios;
        }

        public List<FuncionarioModel> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetFuncionario();
        }

        public List<FuncionarioModel> Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";            
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE CPF LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

           return GetFuncionario();
        }

        public bool Atualizar(FuncionarioModel funcionario)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Nome = @Nome, Telefone = @Telefone, CPF = @CPF, Profissao = @Profissao, Email = @Email, Ativo = @Ativo, Usuario = @Usuario, Senha = @Senha  WHERE Id = @id";
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", funcionario.Id);
            return DadosFuncionario(funcionario);
        }



        public string VerificarLogin(string usuario, string senha)
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = "SELECT * FROM Funcionario WHERE Usuario = @Usuario1 AND Senha = @Senha ";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Usuario1", usuario);
            Cmd.Parameters.AddWithValue("@Senha", senha);

            SqlDataReader rd = Cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd != null)
                {
                    string profissao = (string)rd[nameof(FuncionarioModel.Profissao)];
                    rd.Close();
                    return profissao;
                }
            }
            rd.Close();
            return null;
        }

        static public string GerarHashMd5(string entrada)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Converter a String para array de bytes, que é como a biblioteca trabalha.
                byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(entrada));

                // Cria-se um StringBuilder para recompôr a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop para formatar cada byte como uma String em hexadecimal
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}

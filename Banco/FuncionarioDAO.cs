using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public bool Inserir(FuncionarioModel funcionario)
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Telefone @CPF, @Email, @Profissao, @Ativo, @Usuario, @Senha)";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            Cmd.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
            Cmd.Parameters.AddWithValue("@CPF", funcionario.CPF);
            Cmd.Parameters.AddWithValue("@Email", funcionario.Email);
            Cmd.Parameters.AddWithValue("@Profissao", funcionario.Profissao);
            Cmd.Parameters.AddWithValue("@Ativo", true);
            Cmd.Parameters.AddWithValue("@Usuario", funcionario.Usuario);
            Cmd.Parameters.AddWithValue("@Senha", funcionario.Senha);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        private List<FuncionarioModel> BuscaCliente()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

            while (rd.Read())
            {
                FuncionarioModel funcionario =
                    new FuncionarioModel(
                        (int)rd[nameof(FuncionarioModel.Id)],
                        (string)rd["Nome"],
                        (string)rd["Telefone"],
                        (string)rd["CPF"],
                        (string)rd["Profissao"],
                        (string)rd["Email"],
                        (bool)rd["Ativo"],
                        (string)rd["Usuario"],
                        (string)rd["Senha"]);

                funcionarios.Add(funcionario);
            }
            rd.Close();
            return funcionarios;
        }

        public void Listar()
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = "SELECT * FROM Funcionario";
            BuscaCliente();
        }

        public List<FuncionarioModel> Consultar(string busca)
        {
            busca = $"%{busca}%";
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = "SELECT * FROM Cliente WHERE CPF LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            SqlDataReader rd = Cmd.ExecuteReader();
            List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

            while (rd.Read())
            {

                FuncionarioModel funcionario = new FuncionarioModel((int)rd["Id"], (string)rd["Nome"], (string)rd["Telefone"], (string)rd["CPF"],
                    (string)rd["Profissao"], (string)rd["Email"], (bool)rd["Ativo"], (string)rd["Usuario"], (string)rd["Senha"]);
                funcionarios.Add(funcionario);
            }
            rd.Close();
            return funcionarios;
        }

        public bool Atualizar(ClienteModel cliente)
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = @"UPDATE Cliente SET Nome = @Nome, CPF = @CPF, Email = @Email, DataNascimento = @DataNascimento, Ativo = @Ativo, Sexo = @Sexo, EstadoCivil = @EstadoCivil, Imagem = @Imagem  WHERE Id = @id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", cliente.Id);
            Cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            Cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            Cmd.Parameters.AddWithValue("@Email", cliente.Email);
            Cmd.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(cliente.DataNascimento).ToShortDateString());
            Cmd.Parameters.AddWithValue("@Ativo", cliente.Ativo);
            Cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
            Cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
            Cmd.Parameters.AddWithValue("@Imagem", cliente.Imagem);


            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            return false;
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

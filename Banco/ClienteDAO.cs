using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco
{
    class ClienteDAO
    {
        private readonly string _tabela = "Cliente";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ClienteDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao() //ToDo: nome ideal para esta função
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosCliente(ClienteModel cliente)
        {
            GetConexao();
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            Cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            Cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            Cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
            Cmd.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(cliente.DataNascimento).ToShortDateString());
            Cmd.Parameters.AddWithValue("@Ativo", true);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Inserir(ClienteModel cliente)
        {
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Telefone, @CPF, @Sexo, @DataNascimento, @Ativo)";
            return DadosCliente(cliente);
        }

        private List<ClienteModel> GetCliente()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ClienteModel> clientes = new List<ClienteModel>();

            while (rd.Read())
            {
                ClienteModel cliente = new ClienteModel(
                        (int)rd[nameof(ClienteModel.Id)],
                        (string)rd[nameof(ClienteModel.Nome)],
                        (string)rd[nameof(ClienteModel.Telefone)],
                        (string)rd[nameof(ClienteModel.CPF)],
                        (string)rd[nameof(ClienteModel.Sexo)],
                        (DateTime)rd[nameof(ClienteModel.DataNascimento)],
                        (bool)rd[nameof(ClienteModel.Ativo)]);

                clientes.Add(cliente);
            }
            rd.Close();
            return clientes;
        }

        public List<ClienteModel> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetCliente();
        }

        public List<ClienteModel> Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE CPF LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            return GetCliente();
        }

        public bool Atualizar(ClienteModel cliente)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Nome = @Nome, Telefone = @Telefone, CPF = @CPF, Sexo = @Sexo, DataNascimento = @DataNascimento, Ativo = @Ativo  WHERE Id = @id";

            return DadosCliente(cliente);
        }
    }
}

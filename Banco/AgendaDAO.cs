using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco
{
    class AgendaDAO
    {


        private readonly string _tabela = "Agenda";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public AgendaDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao() //ToDo: nome ideal para esta função
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosAgendamento(AgendaModel agendamento)
        {
            GetConexao();
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Nome", agendamento.Cliente);
            Cmd.Parameters.AddWithValue("@Peso", agendamento.Funcionario);
            Cmd.Parameters.AddWithValue("@Medicao", agendamento.Procedimento);
            Cmd.Parameters.AddWithValue("@Quantidade", agendamento.Horario);
            Cmd.Parameters.AddWithValue("@Ativo", true);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public void Inserir(AgendaModel agendamento)
        {
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Cliente, @Profissional @Procedimento, @Horario, @Ativo)";
            DadosAgendamento(agendamento);
        }

        private List<AgendaModel> GetProduto()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<AgendaModel> agendamentos = new List<AgendaModel>();

            while (rd.Read())
            {
                AgendaModel agendamento = new AgendaModel(
                        (int)rd[nameof(AgendaModel.Id)],
                        (ClienteModel)rd[nameof(AgendaModel.Cliente)],
                        (FuncionarioModel)rd[nameof(AgendaModel.Funcionario)],
                        (ProcedimentoModel)rd[nameof(AgendaModel.Procedimento)],
                        (DateTime)rd[nameof(AgendaModel.Horario)],
                        (bool)rd[nameof(AgendaModel.Ativo)]);

                agendamentos.Add(agendamento);
            }
            rd.Close();
            return agendamentos;
        }

        public void Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            GetProduto();
        }

        public void Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE Id LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            GetProduto();
        }

        public void Atualizar(AgendaModel agendamento)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Cliente = @Cliente, Profissional = @Profissional, Procedimento = @Procedimento, Horario = @Horario, Ativo = @Ativo  WHERE Id = @id";

            DadosAgendamento(agendamento);
        }
    }
}
